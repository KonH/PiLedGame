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
	static class Program {
		static void Main(string[] args) {
			var config = Configuration.Load(ArgumentSet.Parse(args));
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