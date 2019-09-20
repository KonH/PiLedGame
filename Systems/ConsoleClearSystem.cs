using System;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ConsoleClearSystem : ISystem {
		public void Update(GameState state) {
			if ( !state.Debug.IsTriggered ) {
				return;
			}
			Console.Clear();
		}
	}
}