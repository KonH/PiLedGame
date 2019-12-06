using System;
using SimpleECS.Core.Common;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ReadConsoleReadInputSystem : BaseReadInputSystem {
		public override (bool, KeyCode) TryReadKey() {
			if ( !Console.KeyAvailable ) {
				return (false, default);
			}
			return (true, ConvertKey(Console.ReadKey().Key));
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