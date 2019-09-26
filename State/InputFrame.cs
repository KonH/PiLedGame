using System;

namespace PiLedGame.State {
	public sealed class InputFrame {
		public readonly double     Time;
		public readonly ConsoleKey Key;

		public InputFrame(double time, ConsoleKey key) {
			Time = time;
			Key  = key;
		}
	}
}