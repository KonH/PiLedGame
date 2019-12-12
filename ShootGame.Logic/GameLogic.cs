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

		const int PlayerDamageLayer = 1;

		static readonly DamageLayer PlayerLayer = DamageLayer.Of(PlayerDamageLayer);

		const int BulletSpawnRequest      = 1;
		const int BonusBulletSpawnRequest = 2;
		const int ObstacleSpawnRequest    = 3;
		const int HealthSpawnRequest      = 4;
		const int BonusSpawnRequest       = 5;

		static readonly SpawnRequestType Bullet      = SpawnRequestType.Of(BulletSpawnRequest);
		static readonly SpawnRequestType BonusBullet = SpawnRequestType.Of(BonusBulletSpawnRequest);
		static readonly SpawnRequestType Obstacle    = SpawnRequestType.Of(ObstacleSpawnRequest);
		static readonly SpawnRequestType Health      = SpawnRequestType.Of(HealthSpawnRequest);
		static readonly SpawnRequestType Bonus       = SpawnRequestType.Of(BonusSpawnRequest);

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
				e.SolidRender(origin, Color.Green).Falling(0.75).AddComponent<ItemComponent>().Init(HealthItem);
			})),
			new SpawnSystem(new SpawnConfig(Bonus, (e, origin) => {
				e.SolidRender(origin, Color.Red).Falling(0.75).AddComponent<ItemComponent>().Init(BonusItem);
			})),
		};

		public static ISystem SpawnObstacles => new SpawnSystem(new SpawnConfig(Obstacle, (e, origin) => {
			e.SolidRender(origin, Color.Indigo).Falling(0.5);
			e.AddComponent<DamageComponent>().Init();
			e.AddComponent<HealthComponent>().Init();
		}));

		public static ISystem[] PlayerShoots => new ISystem[] {
			new KeyboardSpawnSystem(new KeyboardSpawnConfig(Controls.Shoot, Bullet)),
			new SpawnSystem(new SpawnConfig(Bullet, (e, origin) => {
				e.SolidRender(origin + Point2D.Up, Color.Red).Rising(0.33);
				e.AddComponent<DamageComponent>().Init(layer: PlayerLayer);
				e.AddComponent<TrailComponent>().Init(1.5, Color.Firebrick);
			})),
		};

		public static ISystem[] BonusBulletSpawn => new ISystem[] {
			new UseItemSystem<SpawnBonusBulletEvent>(new UseItemConfig(BonusItem)),
			new SpawnByEventSystem<SpawnBonusBulletEvent>(new SpawnByEventConfig(BonusBullet)),
			new SpawnSystem(new SpawnConfig(BonusBullet, (e, origin) => {
				e.SolidRender(origin, Color.Red).Rising(0.15);
				e.AddComponent<DamageComponent>().Init(layer: PlayerLayer, persistent: true);
				e.AddComponent<TrailComponent>().Init(1.5, Color.Firebrick);
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

		public static ISystem[] CollectScores => new ISystem[] {
			new DestroyUnitScoreSystem(10),
			new CollectItemScoreSystem(50),
			new UpdateScoreSystem(),
		};

		public static ISystem[] RenderTrails => new ISystem[] {
			new AddTrailRenderSystem(2.5),
			new UpdateFadeRenderSystem(),
			new RemoveHiddenFadeRenderSystem(),
		};

		public static void PrepareState(ScreenConfig screen, EntitySet entities) {
			entities.AddTopLine(screen, (e, x, y) => e.Spawn(x, y, Obstacle).AddComponent<RandomSpawnComponent>().Init(2, 5));
			entities.AddTopLine(screen, (e, x, y) => e.Spawn(x, y, Health).AddComponent<RandomSpawnComponent>().Init(20, 40));
			entities.AddTopLine(screen, (e, x, y) => e.Spawn(x, y, Bonus).AddComponent<RandomSpawnComponent>().Init(25, 70));
			entities.AddBottomLine(screen, (e, x, y) => e.Spawn(x, y, BonusBullet));
			var entity = entities.Add();
			entity.SolidRender(new Point2D(4, 1), Color.Green);
			entity.AddComponent<PlayerComponent>();
			entity.AddComponent<SpawnComponent>().Init(Bullet);
			entity.AddComponent<KeyboardSpawnComponent>();
			entity.AddComponent<HealthComponent>().Init(health: 3, layer: PlayerLayer);
			entity.AddComponent<KeyboardMovementComponent>();
			entity.AddComponent<InventoryComponent>();
			entity.AddComponent<FitInsideScreenComponent>();
		}

		static Entity SolidRender(this Entity entity, Point2D origin, Color color) {
			entity.AddComponent<PositionComponent>().Init(origin);
			entity.AddComponent<SolidBodyComponent>();
			entity.AddComponent<RenderComponent>().Init(color);
			return entity;
		}

		static Entity Falling(this Entity entity, double interval) {
			entity.AddComponent<LinearMovementComponent>().Init(Point2D.Down, interval);
			entity.AddComponent<OutOfBoundsDestroyComponent>();
			return entity;
		}

		static void Rising(this Entity entity, double interval) {
			entity.AddComponent<LinearMovementComponent>().Init(Point2D.Up, interval);
			entity.AddComponent<OutOfBoundsDestroyComponent>();
		}

		static Entity Spawn(this Entity entity, int x, int y, SpawnRequestType spawnRequest) {
			entity.AddComponent<PositionComponent>().Init(new Point2D(x, y));
			entity.AddComponent<SpawnComponent>().Init(spawnRequest);
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