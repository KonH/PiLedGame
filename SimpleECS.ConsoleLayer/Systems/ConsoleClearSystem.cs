using System;
using SimpleECS.Core.State;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ConsoleClearSystem : ISystem {
		public void Update(GameState state) {
			if ( !state.Debug.IsTriggered ) {
				return;
			}
			Console.Clear();
		}
	}
}