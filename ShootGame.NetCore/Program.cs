using System;
using ShootGame.Logic.Systems;
using SimpleECS.ConsoleLayer.Utils;

namespace ShootGame.NetCore {
	static class Program {
		static void Main(string[] args) {
			var config = ConfigurationLoader.Load(ArgumentSet.Parse(args));
			var root = new CompositionRoot(config);
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