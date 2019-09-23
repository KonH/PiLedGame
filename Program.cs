using System;
using System.Drawing;
using PiLedGame.Common;
using PiLedGame.State;
using PiLedGame.Systems;
using PiLedGame.Components;
using PiLedGame.Entities;

namespace PiLedGame {
	class Program {
		static void Main(string[] args) {
			var graphics = new Graphics(new Screen(8, 8));
			var debug = new Debug(updateTime: 0.15f, maxLogSize: 20);
			var state = new GameState(graphics, debug);

			using ( var editor = state.Entities.Edit() ) {
				var player = editor.AddEntity();
				player.AddComponent(new PositionComponent(new Point2D(4, 6)));
				player.AddComponent(new RenderComponent(Color.Green));
				player.AddComponent(new SpawnComponent(SpawnBullet, Point2D.Up));
				player.AddComponent(new HealthComponent(health: 3, layer: "player"));
				player.AddComponent(new PlayerComponent());
				player.AddComponent(new KeyboardMovementComponent(MovePlayer));
				player.AddComponent(new KeyboardSpawnComponent(ConsoleKey.Spacebar));

				for ( var i = 0; i < graphics.Screen.Width; i++ ) {
					var trigger = editor.AddEntity();
					trigger.AddComponent(new PositionComponent(new Point2D(i, 0)));
					trigger.AddComponent(new SpawnComponent(SpawnObstacle));
					trigger.AddComponent(new RandomSpawnComponent(2, 5));
				}

				for ( var i = 0; i < graphics.Screen.Width; i++ ) {
					var trigger = editor.AddEntity();
					trigger.AddComponent(new PositionComponent(new Point2D(i, 7)));
					trigger.AddComponent(new SpawnComponent(SpawnBonusBullet, Point2D.Up));
					trigger.AddComponent(new KeyboardSpawnComponent(ConsoleKey.Z));
				}
			}

			var systems = new SystemSet(
				new WaitForTargetFpsSystem(60),
				new ResetInputSystem(),
				new ReadInputSystem(),
				new ClearFrameSystem(),
				new PlayerMovementSystem(),
				new ShootTriggerSystem(),
				new RandomSpawnSystem(),
				new SpawnSystem(),
				new LinearMovementSystem(),
				new DamageSystem(),
				new NoHealthDestroySystem(),
				new OutOfBoundsDestroySystem(),
				new DestroyTriggeredDamageSystem(),
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
				new DeviceRenderSystem(state)
			);
			systems.UpdateLoop(state);
		}

		static void SpawnBullet(Entity bullet, Point2D origin, Point2D direction) {
			bullet.AddComponent(new PositionComponent(origin + direction));
			bullet.AddComponent(new RenderComponent(Color.Red));
			bullet.AddComponent(new TrailComponent(1.5, Color.Firebrick));
			bullet.AddComponent(new LinearMovementComponent(direction, 0.33));
			bullet.AddComponent(new OutOfBoundsDestroyComponent());
			bullet.AddComponent(new DamageComponent(layer: "player"));
		}

		static void SpawnBonusBullet(Entity bullet, Point2D origin, Point2D direction) {
			bullet.AddComponent(new PositionComponent(origin + direction));
			bullet.AddComponent(new RenderComponent(Color.Red));
			bullet.AddComponent(new TrailComponent(1.5, Color.Firebrick));
			bullet.AddComponent(new LinearMovementComponent(direction, 0.15));
			bullet.AddComponent(new OutOfBoundsDestroyComponent());
			bullet.AddComponent(new DamageComponent(layer: "player", persistent: true));
		}

		static void SpawnObstacle(Entity obstacle, Point2D origin, Point2D direction) {
			obstacle.AddComponent(new PositionComponent(origin + direction));
			obstacle.AddComponent(new RenderComponent(Color.Indigo));
			obstacle.AddComponent(new LinearMovementComponent(Point2D.Down, 0.5));
			obstacle.AddComponent(new OutOfBoundsDestroyComponent());
			obstacle.AddComponent(new DamageComponent());
			obstacle.AddComponent(new HealthComponent());
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