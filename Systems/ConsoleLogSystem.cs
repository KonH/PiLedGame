using System;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ConsoleLogSystem : ISystem {
		public void Update(GameState state) {
			if ( !state.Debug.IsTriggered ) {
				return;
			}
			Console.WriteLine();
			Console.WriteLine("OUTPUT:");
			foreach ( var line in state.Debug.GetContent() ) {
				Console.WriteLine(line);
			}
		}
	}
}