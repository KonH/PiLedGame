using System.Drawing;
using PiLedGame.Common;
using PiLedGame.State;
using PiLedGame.Systems;
using PiLedGame.Components;

namespace PiLedGame {
	class Program {
		static void Main(string[] args) {
			var graphics = new Graphics(new Screen(8, 8));
			var debug = new Debug(updateTime: 0.15f, maxLogSize: 20);
			var state = new GameState(graphics, debug);

			var player = state.Entities.AddEntity();
			player.AddComponent(new PositionComponent(new Point2D(4, 4)));
			player.AddComponent(new RenderComponent(Color.Green));
			player.AddComponent(new KeyboardControlComponent());
			player.AddComponent(new SpawnSourceComponent());

			state.Entities.FlushNewEntities();

			var systems = new SystemSet(
				new ResetInputSystem(),
				new ReadInputSystem(),
				new ClearFrameSystem(),
				new PlayerMovementSystem(),
				new SpawnSystem(),
				new LinearMovementSystem(),
				new OutOfBoundsDestroySystem(),
				new TrailRenderSystem(300),
				new RenderFrameSystem(),
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
	}
}