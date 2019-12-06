using System;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ConsoleLogSystem : SingleComponentSystem<DebugState> {
		public override void Update(DebugState debug) {
			if ( !debug.IsTriggered ) {
				return;
			}
			Console.WriteLine();
			Console.WriteLine("OUTPUT:");
			foreach ( var line in debug.GetContent() ) {
				Console.WriteLine(line);
			}
		}
	}
}