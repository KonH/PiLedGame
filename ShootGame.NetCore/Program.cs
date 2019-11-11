using System;
using SimpleECS.Core.Systems;
using SimpleECS.ConsoleLayer.Systems;
using SimpleECS.ConsoleLayer.Utils;
using SimpleECS.RaspberryPi.Systems;
using ShootGame.Logic;
using ShootGame.Logic.Systems;

namespace ShootGame.NetCore {
	static class Program {
		static void Main(string[] args) {
			var config = ConfigurationLoader.Load(ArgumentSet.Parse(args));
			var root = new CompositionRoot(
				config,
				() => new ReadConsoleReadInputSystem(),
				new Func<ISystem>[] {
					() => new ConsoleTriggerSystem(),
					() => new ConsoleClearSystem(),
					() => new ConsoleRenderSystem(),
					() => new ConsoleLogSystem()
				},
				() => new FinishFrameRealTimeSystem(),
				() => new DeviceRenderSystem(172));
			var (state, systems) = root.Create();
			systems.Init(state);
			systems.UpdateLoop(state);
			Console.WriteLine($"Your score is: {systems.Get<ScoreMeasureSystem>()?.TotalScore}");
			Console.WriteLine($"Time: {state.Time.UnscaledTotalTime:0.00}");
			if ( config.IsReplayRecord ) {
				config.NewReplayRecord.Save(config.NewReplayPath);
			}
		}
	}
}