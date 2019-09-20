using System.Drawing;
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
			player.AddComponent(new PositionComponent());
			player.AddComponent(new RenderComponent(Color.Green));
			player.AddComponent(new KeyboardControlComponent());
			player.AddComponent(new TrailComponent(Color.Purple));

			var systems = new SystemSet(
				new ResetInputSystem(),
				new ReadInputSystem(),
				new ClearFrameSystem(),
				new PlayerMovementSystem(),
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