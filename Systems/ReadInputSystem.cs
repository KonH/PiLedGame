using System;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ReadInputSystem : ISystem {
		public void Update(GameState state) {
			if ( Console.KeyAvailable ) {
				var key = Console.ReadKey().Key;
				state.Input.Assign(key);
			}
		}
	}
}