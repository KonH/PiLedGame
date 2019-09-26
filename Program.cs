using System;
using System.Diagnostics;
using System.Drawing;
using PiLedGame.Common;
using PiLedGame.State;
using PiLedGame.Systems;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.Events;
using Debug = PiLedGame.State.Debug;

namespace PiLedGame {
	class Program {
		static bool UseReplay {
			get { return true; }
		}

		const string PlayerLayer = "player";
		const string Bullet      = "bullet";
		const string BonusBullet = "bonusBullet";
		const string Obstacle    = "obstacle";
		const string HealthItem  = "health";
		const string BonusItem   = "bonus";

		static void Main(string[] args) {
			var graphics = new Graphics(new Screen(8, 8));
			var debug = new Debug(updateTime: 0.15f, maxLogSize: 20);
			var random = UseReplay ? new Random(0) : new Random();
			var state = new GameState(graphics, debug, random);
			var scoreMeasure = new ScoreMeasureSystem();
			var timer = new Stopwatch();
			var record = GetInputRecord();
			var systems = CreateSystems(state, timer, scoreMeasure, record);
			InitStartupEntities(state);
			timer.Start();
			systems.UpdateLoop(state);
			Console.WriteLine($"Your score is: {scoreMeasure.TotalScore}");
			Console.WriteLine($"Time: {state.Time.UnscaledTotalTime:0.00}");
			foreach ( var frame in record.Frames ) {
				Console.WriteLine($"new InputFrame({frame.Time}, ConsoleKey.{frame.Key}),");
			}
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

		static SystemSet CreateSystems(GameState state, Stopwatch timer, ScoreMeasureSystem scoreMeasure, InputRecord record) {
			return new SystemSet(
				new WaitForTargetFpsSystem(60),
				new SpeedUpSystem(10.0, 0.25),
				new ResetInputSystem(),
				UseReplay ? new FixedInputSystem(record) as ISystem : new ReadConsoleInputSystem(),
				new SaveInputSystem(record),
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
				UseReplay ? new FinishFrameFixedTimeSystem(0.0005) as ISystem : new FinishFrameRealTimeSystem(timer),
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

		static InputRecord GetInputRecord() {
			return new InputRecord(
				new InputFrame(3.63800000000038, ConsoleKey.Spacebar),
				new InputFrame(7.73149999999843, ConsoleKey.RightArrow),
				new InputFrame(8.84649999999932, ConsoleKey.Spacebar),
				new InputFrame(11.0850000000021, ConsoleKey.Spacebar),
				new InputFrame(11.864500000003, ConsoleKey.LeftArrow),
				new InputFrame(12.3660000000036, ConsoleKey.Spacebar),
				new InputFrame(14.0425000000057, ConsoleKey.RightArrow),
				new InputFrame(14.2690000000059, ConsoleKey.Spacebar),
				new InputFrame(17.2490000000052, ConsoleKey.Spacebar),
				new InputFrame(18.2820000000027, ConsoleKey.RightArrow),
				new InputFrame(18.8690000000014, ConsoleKey.Spacebar),
				new InputFrame(20.1774999999983, ConsoleKey.LeftArrow),
				new InputFrame(20.5139999999975, ConsoleKey.Spacebar),
				new InputFrame(21.163999999996, ConsoleKey.RightArrow),
				new InputFrame(22.3869999999932, ConsoleKey.Spacebar),
				new InputFrame(23.7249999999901, ConsoleKey.RightArrow),
				new InputFrame(27.8554999999804, ConsoleKey.LeftArrow),
				new InputFrame(28.2464999999795, ConsoleKey.LeftArrow),
				new InputFrame(28.5299999999789, ConsoleKey.LeftArrow),
				new InputFrame(28.905999999978, ConsoleKey.Spacebar),
				new InputFrame(30.4394999999744, ConsoleKey.Spacebar),
				new InputFrame(31.6899999999715, ConsoleKey.Spacebar),
				new InputFrame(32.4974999999731, ConsoleKey.LeftArrow),
				new InputFrame(33.5829999999783, ConsoleKey.Spacebar),
				new InputFrame(36.3724999999916, ConsoleKey.Spacebar),
				new InputFrame(37.6144999999976, ConsoleKey.Spacebar),
				new InputFrame(38.7215000000029, ConsoleKey.RightArrow),
				new InputFrame(40.7860000000127, ConsoleKey.Spacebar));
		}
	}
}