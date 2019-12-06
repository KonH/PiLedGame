using System;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ConsoleClearSystem : SingleComponentSystem<DebugState> {
		public override void Update(DebugState debug) {
			if ( !debug.IsTriggered ) {
				return;
			}
			Console.Clear();
		}
	}
}