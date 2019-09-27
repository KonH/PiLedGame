using System;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ReadConsoleInputSystem : ISystem {
		public void Update(GameState state) {
			if ( Console.KeyAvailable ) {
				var key = Console.ReadKey().Key;
				state.Input.Assign(ConvertKey(key));
			}
		}

		KeyCode ConvertKey(ConsoleKey consoleKey) {
			var str = consoleKey.ToString();
			try {
				return Enum.Parse<KeyCode>(str);
			} catch {
				return KeyCode.None;
			}
		}
	}
}