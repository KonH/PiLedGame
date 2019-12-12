using System;
using SimpleECS.Core.Systems;
using SimpleECS.ConsoleLayer.Systems;
using SimpleECS.ConsoleLayer.Utils;
using SimpleECS.RaspberryPi.Systems;
using ShootGame.Logic;
using ShootGame.Logic.States;
using SimpleECS.Core.Configs;
using SimpleECS.Core.States;
using SimpleECS.RaspberryPi.Configs;

namespace ShootGame.NetCore {
	static class Program {
		static void Main(string[] args) {
			var screen = new ScreenConfig(8, 8);
			var debug = new DebugConfig(updateTime: 0.15f, maxLogSize: 20);
			var config = ConfigurationLoader.Load(ArgumentSet.Parse(args));
			var root = new CompositionRoot(
				screen,
				debug,
				config,
				() => new ReadConsoleReadInputSystem(),
				new Func<ISystem>[] {
					() => new ConsoleTriggerSystem(debug),
					() => new ConsoleClearSystem(),
					() => new ConsoleRenderSystem(screen),
					() => new ConsoleLogSystem()
				},
				() => new FinishFrameRealTimeSystem(),
				() => new DeviceRenderSystem(screen, new DeviceRenderConfig(172)));
			var (entities, systems) = root.Create();
			systems.Init(entities);
			systems.UpdateLoop(entities);
			Console.WriteLine($"Your score is: {entities.GetFirstComponent<ScoreState>()?.TotalScore}");
			Console.WriteLine($"Time: {entities.GetFirstComponent<TimeState>().UnscaledTotalTime:0.00}");
			if ( config.IsReplayRecord ) {
				var replay = entities.GetFirstComponent<InputRecordState>();
				replay.Save(config.NewReplayPath);
			}
		}
	}
}