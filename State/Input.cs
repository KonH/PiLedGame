using System;

namespace PiLedGame.State {
	public sealed class Input {
		public ConsoleKey Current { get; private set; }

		public void Assign(ConsoleKey key) {
			Current = key;
		}

		public void Reset() {
			Current = default;
		}
	}
}