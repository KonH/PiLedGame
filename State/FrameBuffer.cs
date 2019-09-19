namespace PiLedGame.State {
	public sealed class FrameBuffer {
		public Frame Current;
		public Frame Next;

		public FrameBuffer(Screen screen) {
			Current = new Frame(screen);
			Next    = new Frame(screen);
		}

		public void Swap() {
			var tmp = Current;
			Current = Next;
			Next = tmp;
		}
	}
}