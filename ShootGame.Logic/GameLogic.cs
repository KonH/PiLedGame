using System;
using ShootGame.Logic.Configs;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;
using ShootGame.Logic.Events;
using ShootGame.Logic.Systems;
using SimpleECS.Core.Configs;

namespace ShootGame.Logic {
	public static class GameLogic {
		public static class Controls {
			public const KeyCode MoveLeft  = KeyCode.LeftArrow;
			public const KeyCode MoveRight = KeyCode.RightArrow;
			public const KeyCode Shoot     = KeyCode.Spacebar;
		}

		static readonly DamageLayer PlayerLayer = DamageLayer.Of("playerLayer");

		static readonly SpawnRequestType Bullet      = SpawnRequestType.Of("bulletSpawn");
		static readonly SpawnRequestType BonusBullet = SpawnRequestType.Of("bonusBulletSpawn");
		static readonly SpawnRequestType Obstacle    = SpawnRequestType.Of("obstacleSpawn");
		static readonly SpawnRequestType Health      = SpawnRequestType.Of("healthSpawn");
		static readonly SpawnRequestType Bonus       = SpawnRequestType.Of("bonusSpawn");

		static readonly ItemType HealthItem = ItemType.Of("healthItem");
		static readonly ItemType BonusItem  = ItemType.Of("bonusItem");

		public static ISystem PlayerKeyboardMovement => new KeyboardMovementSystem(
			new KeyboardMovementConfig((key) => {
				switch ( key ) {
					case Controls.MoveLeft: return Point2D.Left;
					case Controls.MoveRight: return Point2D.Right;
				}
				return default;
			}
		));

		public static ISystem PreventSpawnCollisions =>
			new PreventSpawnCollisionSystem(new PreventSpawnCollisionConfig(Obstacle, Health, Bonus));

		public static ISystem PreventHealthSpawnIfNotRequired =>
			new PreventHealthSpawnSystem(new PreventHealthSpawnConfig(Health, 3));

		public static ISystem[] SpawnItems => new ISystem[] {
			new SpawnSystem(new SpawnConfig(Health, (e, origin) => {
				e.SolidRender(origin, Color.Green).Falling(0.75).With(new ItemComponent(HealthItem));
			})),
			new SpawnSystem(new SpawnConfig(Bonus, (e, origin) => {
				e.SolidRender(origin, Color.Red).Falling(0.75).With(new ItemComponent(BonusItem));
			})),
		};

		public static ISystem SpawnObstacles => new SpawnSystem(new SpawnConfig(Obstacle, (e, origin) => {
			e.SolidRender(origin, Color.Indigo).Falling(0.5).With(new DamageComponent()).With(new HealthComponent());
		}));

		public static ISystem[] PlayerShoots => new ISystem[] {
			new KeyboardSpawnSystem(new KeyboardSpawnConfig(Controls.Shoot, Bullet)),
			new SpawnSystem(new SpawnConfig(Bullet, (e, origin) => {
				e.SolidRender(origin + Point2D.Up, Color.Red).Rising(0.33)
					.With(new DamageComponent(layer: PlayerLayer))
					.With(new TrailComponent(1.5, Color.Firebrick));
			})),
		};

		public static ISystem[] BonusBulletSpawn => new ISystem[] {
			new UseItemSystem<SpawnBonusBulletEvent>(new UseItemConfig(BonusItem)),
			new SpawnByEventSystem<SpawnBonusBulletEvent>(new SpawnByEventConfig(BonusBullet)),
			new SpawnSystem(new SpawnConfig(BonusBullet, (e, origin) => {
				e.SolidRender(origin, Color.Red).Rising(0.15)
					.With(new DamageComponent(layer: PlayerLayer, persistent: true))
					.With(new TrailComponent(1.5, Color.Firebrick));
			})),
		};

		public static ISystem Healing =>
			new UseItemSystem<AddHealthEvent>(new UseItemConfig(HealthItem));

		public static ISystem HealthUI =>
			new RenderPlayerHealthSystem(new RenderPlayerHealthConfig(
				new Point2D(0, 0), Point2D.Right, health => {
					if ( health >= 3 ) {
						return Color.Green;
					}
					return (health == 2) ? Color.Yellow : Color.Red;
				}
		));

		public static void PrepareState(ScreenConfig screen, EntitySet entities) {
			entities.AddTopLine(screen, (e, x, y) => e.Spawn(x, y, Obstacle).With(new RandomSpawnComponent(2, 5)));
			entities.AddTopLine(screen, (e, x, y) => e.Spawn(x, y, Health).With(new RandomSpawnComponent(20, 40)));
			entities.AddTopLine(screen, (e, x, y) => e.Spawn(x, y, Bonus).With(new RandomSpawnComponent(25, 70)));
			entities.AddBottomLine(screen, (e, x, y) => e.Spawn(x, y, BonusBullet));
			entities.Add().SolidRender(new Point2D(4, 1), Color.Green)
				.With(new PlayerComponent())
				.With(new SpawnComponent(Bullet))
				.With(new KeyboardSpawnComponent())
				.With(new HealthComponent(health: 3, layer: PlayerLayer))
				.With(new KeyboardMovementComponent())
				.With(new InventoryComponent())
				.With(new FitInsideScreenComponent());
		}

		static Entity SolidRender(this Entity entity, Point2D origin, Color color) {
			return entity
				.With(new PositionComponent(origin))
				.With(new SolidBodyComponent())
				.With(new RenderComponent(color));
		}

		static Entity Falling(this Entity entity, double interval) {
			return entity
				.With(new LinearMovementComponent(Point2D.Down, interval))
				.With(new OutOfBoundsDestroyComponent());
		}

		static Entity Rising(this Entity entity, double interval) {
			return entity
				.With(new LinearMovementComponent(Point2D.Up, interval))
				.With(new OutOfBoundsDestroyComponent());
		}

		static Entity With(this Entity entity, IComponent component) {
			entity.AddComponent(component);
			return entity;
		}

		static Entity Spawn(this Entity entity, int x, int y, SpawnRequestType spawnRequest) {
			entity.AddComponent(new PositionComponent(new Point2D(x, y)));
			entity.AddComponent(new SpawnComponent(spawnRequest));
			return entity;
		}

		static void AddTopLine(this EntitySet entities, ScreenConfig screen, Action<Entity, int, int> entityCtor) {
			for ( var i = 0; i < screen.Width; i++ ) {
				entityCtor(entities.Add(), i, screen.Height - 1);
			}
		}

		static void AddBottomLine(this EntitySet entities, ScreenConfig screen, Action<Entity, int, int> entityCtor) {
			for ( var i = 0; i < screen.Width; i++ ) {
				entityCtor(entities.Add(), i, 0);
			}
		}
	}
}