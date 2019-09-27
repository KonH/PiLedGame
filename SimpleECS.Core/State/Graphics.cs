namespace SimpleECS.Core.State {
	public sealed class Graphics {
		public Screen Screen { get; }
		public Frame  Frame  { get; }

		public Graphics(Screen screen) {
			Screen = screen;
			Frame  = new Frame(Screen);
		}
	}
}