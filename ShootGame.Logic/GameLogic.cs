using System;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;
using ShootGame.Logic.Events;
using ShootGame.Logic.Systems;

namespace ShootGame.Logic {
	public static class GameLogic {
		static readonly DamageLayer PlayerLayer = DamageLayer.Of("playerLayer");

		static readonly SpawnRequestType Bullet      = SpawnRequestType.Of("bulletSpawn");
		static readonly SpawnRequestType BonusBullet = SpawnRequestType.Of("bonusBulletSpawn");
		static readonly SpawnRequestType Obstacle    = SpawnRequestType.Of("obstacleSpawn");
		static readonly SpawnRequestType Health      = SpawnRequestType.Of("healthSpawn");
		static readonly SpawnRequestType Bonus       = SpawnRequestType.Of("bonusSpawn");

		static readonly ItemType HealthItem = ItemType.Of("healthItem");
		static readonly ItemType BonusItem  = ItemType.Of("bonusItem");

		public static ISystem PlayerKeyboardMovement => new KeyboardMovementSystem(key => {
			switch ( key ) {
				case KeyCode.LeftArrow: return Point2D.Left;
				case KeyCode.RightArrow: return Point2D.Right;
			}
			return default;
		});

		public static ISystem PreventSpawnCollisions => new PreventSpawnCollisionSystem(Obstacle, Health, Bonus);

		public static ISystem PreventHealthSpawnIfNotRequired => new PreventHealthSpawnSystem(Health, 3);

		public static ISystem[] SpawnItems => new ISystem[] {
			new SpawnSystem(Health, (e, origin) => {
				e.SolidRender(origin, Color.Green).Falling(0.75).With(new ItemComponent(HealthItem));
			}),
			new SpawnSystem(Bonus, (e, origin) => {
				e.SolidRender(origin, Color.Red).Falling(0.75).With(new ItemComponent(BonusItem));
			}),
		};

		public static ISystem SpawnObstacles => new SpawnSystem(Obstacle, (e, origin) => {
			e.SolidRender(origin, Color.Indigo).Falling(0.5).With(new DamageComponent()).With(new HealthComponent());
		});

		public static ISystem[] PlayerShoots => new ISystem[] {
			new KeyboardSpawnSystem(KeyCode.Spacebar, Bullet),
			new SpawnSystem(Bullet, (e, origin) => {
				e.SolidRender(origin + Point2D.Up, Color.Red).Rising(0.33)
					.With(new DamageComponent(layer: PlayerLayer))
					.With(new TrailComponent(1.5, Color.Firebrick));
			}),
		};

		public static ISystem[] BonusBulletSpawn => new ISystem[] {
			new UseItemSystem<SpawnBonusBulletEvent>(BonusItem),
			new SpawnByEventSystem<SpawnBonusBulletEvent>(BonusBullet),
			new SpawnSystem(BonusBullet, (e, origin) => {
				e.SolidRender(origin, Color.Red).Rising(0.15)
					.With(new DamageComponent(layer: PlayerLayer, persistent: true))
					.With(new TrailComponent(1.5, Color.Firebrick));
			}),
		};

		public static ISystem Healing => new UseItemSystem<AddHealthEvent>(HealthItem);

		public static ISystem HealthUI =>
			new RenderPlayerHealthSystem(new Point2D(0, 7), Point2D.Right, health => {
				if ( health >= 3 ) {
					return Color.Green;
				}
				return (health == 2) ? Color.Yellow : Color.Red;
			});

		public static void PrepareState(GameState state) {
			state.AddTopLine((e, x, y) => e.Spawn(x, y, Obstacle).With(new RandomSpawnComponent(2, 5)));
            state.AddTopLine((e, x, y) => e.Spawn(x, y, Health).With(new RandomSpawnComponent(20, 40)));
            state.AddTopLine((e, x, y) => e.Spawn(x, y, Bonus).With(new RandomSpawnComponent(25, 70)));
            state.AddBottomLine((e, x, y) => e.Spawn(x, y, BonusBullet));
            using ( var editor = state.Entities.Edit() ) {
              editor.AddEntity().SolidRender(new Point2D(4, 6), Color.Green)
	              .With(new PlayerComponent())
	              .With(new SpawnComponent(Bullet))
	              .With(new KeyboardSpawnComponent())
	              .With(new HealthComponent(health: 3, layer: PlayerLayer))
	              .With(new KeyboardMovementComponent())
	              .With(new InventoryComponent())
	              .With(new FitInsideScreenComponent());
            }
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

		static void AddTopLine(this GameState state, Action<Entity, int, int> entityCtor) {
			using ( var editor = state.Entities.Edit() ) {
				for ( var i = 0; i < state.Graphics.Screen.Width; i++ ) {
					entityCtor(editor.AddEntity(), i, 0);
				}
			}
		}

		static void AddBottomLine(this GameState state, Action<Entity, int, int> entityCtor) {
			using ( var editor = state.Entities.Edit() ) {
				var screen = state.Graphics.Screen;
				for ( var i = 0; i < screen.Width; i++ ) {
					entityCtor(editor.AddEntity(), i, screen.Height - 1);
				}
			}
		}
	}
}