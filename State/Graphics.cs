namespace PiLedGame.State {
	public sealed class Graphics {
		public Screen      Screen      { get; }
		public FrameBuffer FrameBuffer { get; }

		public Frame WorkFrame   => FrameBuffer.Next;
		public Frame RenderFrame => FrameBuffer.Current;

		public Graphics(Screen screen) {
			Screen      = screen;
			FrameBuffer = new FrameBuffer(Screen);
		}
	}
}