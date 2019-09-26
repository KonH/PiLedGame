using System.Collections.Generic;

namespace PiLedGame.State {
	public sealed class InputRecord {
		public readonly List<InputFrame> Frames;

		public InputRecord(params InputFrame[] frameKeys) {
			Frames = new List<InputFrame>(frameKeys);
		}
	}
}