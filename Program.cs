using System;
using System.Drawing;
using PiLedGame.Common;
using PiLedGame.State;
using PiLedGame.Systems;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.Events;

namespace PiLedGame {
	class Program {
		const string PlayerLayer = "player";
		const string Bullet      = "bullet";
		const string BonusBullet = "bonusBullet";
		const string Obstacle    = "obstacle";
		const string HealthItem  = "health";
		const string BonusItem   = "bonus";

		static void Main(string[] args) {
			var graphics = new Graphics(new Screen(8, 8));
			var debug = new Debug(updateTime: 0.15f, maxLogSize: 20);
			var state = new GameState(graphics, debug);
			var scoreMeasure = new ScoreMeasureSystem();
			var systems = CreateSystems(state, scoreMeasure);
			InitStartupEntities(state);
			systems.UpdateLoop(state);
			Console.WriteLine($"Your score is: {scoreMeasure.TotalScore}");
		}

		static void InitStartupEntities(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				AddPlayer(editor);
				var width = state.Graphics.Screen.Width;
				AddBonusBulletSpawners(editor, width);
				AddObstacleSpawners(editor, width);
				AddHealthSpawners(editor, width);
				AddBonusSpawners(editor, width);
			}
		}

		static void AddPlayer(EntityEditor editor) {
			var player = editor.AddEntity();
			player.AddComponent(new PositionComponent(new Point2D(4, 6)));
			player.AddComponent(new SolidBodyComponent());
			player.AddComponent(new RenderComponent(Color.Green));
			player.AddComponent(new SpawnComponent(Bullet));
			player.AddComponent(new KeyboardSpawnComponent());
			player.AddComponent(new HealthComponent(health: 3, layer: PlayerLayer));
			player.AddComponent(new PlayerComponent());
			player.AddComponent(new KeyboardMovementComponent());
			player.AddComponent(new InventoryComponent());
			player.AddComponent(new FitInsideScreenComponent());
		}

		static void AddObstacleSpawners(EntityEditor editor, int width) {
			for ( var i = 0; i < width; i++ ) {
				var trigger = editor.AddEntity();
				trigger.AddComponent(new PositionComponent(new Point2D(i, 0)));
				trigger.AddComponent(new SpawnComponent(Obstacle));
				trigger.AddComponent(new RandomSpawnComponent(2, 5));
			}
		}

		static void AddBonusBulletSpawners(EntityEditor editor, int width) {
			for ( var i = 0; i < width; i++ ) {
				var trigger = editor.AddEntity();
				trigger.AddComponent(new PositionComponent(new Point2D(i, 7)));
				trigger.AddComponent(new SpawnComponent(BonusBullet));
			}
		}

		static void AddHealthSpawners(EntityEditor editor, int width) {
			for ( var i = 0; i < width; i++ ) {
				var trigger = editor.AddEntity();
				trigger.AddComponent(new PositionComponent(new Point2D(i, 0)));
				trigger.AddComponent(new SpawnComponent(HealthItem));
				trigger.AddComponent(new RandomSpawnComponent(20, 40));
			}
		}

		static void AddBonusSpawners(EntityEditor editor, int width) {
			for ( var i = 0; i < width; i++ ) {
				var trigger = editor.AddEntity();
				trigger.AddComponent(new PositionComponent(new Point2D(i, 0)));
				trigger.AddComponent(new SpawnComponent(BonusItem));
				trigger.AddComponent(new RandomSpawnComponent(25, 70));
			}
		}

		static SystemSet CreateSystems(GameState state, ScoreMeasureSystem scoreMeasure) {
			return new SystemSet(
				new WaitForTargetFpsSystem(60),
				new SpeedUpSystem(10.0, 0.25),
				new ResetInputSystem(),
				new ReadInputSystem(),
				new ClearFrameSystem(),
				new KeyboardMovementSystem(MovePlayer),
				new InitRandomSpawnTimerSystem(),
				new UpdateTimerSystem(),
				new TimerTickSystem(),
				new TimerDestroySystem(),
				new PerformRandomSpawnSystem(),
				new LinearMovementSystem(),
				new EventMovementSystem(),
				new FitInsideScreenSystem(),
				new CollisionSystem(),
				new PreventSpawnCollisionSystem(Obstacle, HealthItem, BonusItem),
				new PreventHealthSpawnSystem(HealthItem, 3),
				new SpawnSystem(HealthItem, SpawnHealth),
				new SpawnSystem(BonusItem, SpawnBonus),
				new SpawnSystem(Obstacle, SpawnObstacle),
				new KeyboardSpawnSystem(ConsoleKey.Spacebar, Bullet),
				new SpawnSystem(Bullet, SpawnBullet),
				new UseItemSystem<SpawnBonusBulletEvent>(BonusItem),
				new SpawnByEventSystem<SpawnBonusBulletEvent>(BonusBullet),
				new SpawnSystem(BonusBullet, SpawnBonusBullet),
				new SendDamageSystem(),
				new ApplyDamageSystem(),
				new CollectItemSystem(),
				new AddToInventorySystem(),
				new UseItemSystem<AddHealthEvent>(HealthItem),
				new AddHealthSystem(),
				scoreMeasure,
				new DestroyCollectedItemSystem(),
				new NoHealthDestroySystem(),
				new OutOfBoundsDestroySystem(),
				new DestroyTriggeredDamageSystem(),
				new DestroySystem(),
				new GameOverSystem(),
				new TrailRenderSystem(),
				new RenderFrameSystem(),
				new RenderPlayerHealthSystem(new Point2D(0, 7), Point2D.Right, GetColorByHealth),
				new FinishExecutionSystem(),
				new ConsoleTriggerSystem(),
				new ConsoleClearSystem(),
				new ConsoleRenderSystem(),
				new ConsoleLogSystem(),
				new FinishFrameSystem(),
				new DeviceRenderSystem(state, 172),
				new CleanUpEventSystem()
			);
		}

		static void SpawnBullet(Entity bullet, Point2D origin) {
			var direction = Point2D.Up;
			bullet.AddComponent(new PositionComponent(origin + direction));
			bullet.AddComponent(new SolidBodyComponent());
			bullet.AddComponent(new RenderComponent(Color.Red));
			bullet.AddComponent(new TrailComponent(1.5, Color.Firebrick));
			bullet.AddComponent(new LinearMovementComponent(direction, 0.33));
			bullet.AddComponent(new OutOfBoundsDestroyComponent());
			bullet.AddComponent(new DamageComponent(layer: PlayerLayer));
		}

		static void SpawnBonusBullet(Entity bullet, Point2D origin) {
			bullet.AddComponent(new PositionComponent(origin));
			bullet.AddComponent(new SolidBodyComponent());
			bullet.AddComponent(new RenderComponent(Color.Red));
			bullet.AddComponent(new TrailComponent(1.5, Color.Firebrick));
			bullet.AddComponent(new LinearMovementComponent(Point2D.Up, 0.15));
			bullet.AddComponent(new OutOfBoundsDestroyComponent());
			bullet.AddComponent(new DamageComponent(layer: PlayerLayer, persistent: true));
		}

		static void SpawnObstacle(Entity obstacle, Point2D origin) {
			obstacle.AddComponent(new PositionComponent(origin));
			obstacle.AddComponent(new SolidBodyComponent());
			obstacle.AddComponent(new RenderComponent(Color.Indigo));
			obstacle.AddComponent(new LinearMovementComponent(Point2D.Down, 0.5));
			obstacle.AddComponent(new OutOfBoundsDestroyComponent());
			obstacle.AddComponent(new DamageComponent());
			obstacle.AddComponent(new HealthComponent());
		}

		static void SpawnHealth(Entity health, Point2D origin) {
			health.AddComponent(new PositionComponent(origin));
			health.AddComponent(new SolidBodyComponent());
			health.AddComponent(new RenderComponent(Color.Green));
			health.AddComponent(new LinearMovementComponent(Point2D.Down, 0.75));
			health.AddComponent(new OutOfBoundsDestroyComponent());
			health.AddComponent(new ItemComponent(HealthItem));
		}

		static void SpawnBonus(Entity bonus, Point2D origin) {
			bonus.AddComponent(new PositionComponent(origin));
			bonus.AddComponent(new SolidBodyComponent());
			bonus.AddComponent(new RenderComponent(Color.Red));
			bonus.AddComponent(new LinearMovementComponent(Point2D.Down, 0.75));
			bonus.AddComponent(new OutOfBoundsDestroyComponent());
			bonus.AddComponent(new ItemComponent(BonusItem, 8));
		}

		static Color GetColorByHealth(int health) {
			if ( health >= 3 ) {
				return Color.Green;
			}
			return (health == 2) ? Color.Yellow : Color.Red;
		}

		static Point2D MovePlayer(ConsoleKey key) {
			switch ( key ) {
				case ConsoleKey.LeftArrow: return Point2D.Left;
				case ConsoleKey.RightArrow: return Point2D.Right;
			}
			return default;
		}
	}
}