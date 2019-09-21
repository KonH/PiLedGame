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

			using ( var editor = state.Entities.Edit() ) {
				var player = editor.AddEntity();
				player.AddComponent(new PositionComponent(new Point2D(4, 4)));
				player.AddComponent(new RenderComponent(Color.Green));
				player.AddComponent(new KeyboardControlComponent());
				player.AddComponent(new SpawnSourceComponent());
			}

			var systems = new SystemSet(
				new ResetInputSystem(),
				new ReadInputSystem(),
				new ClearFrameSystem(),
				new PlayerMovementSystem(),
				new SpawnSystem(),
				new LinearMovementSystem(),
				new OutOfBoundsDestroySystem(),
				new TrailRenderSystem(),
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