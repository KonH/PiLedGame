namespace PiLedGame.State {
	public sealed class GameState {
		public Graphics Graphics { get; }

		public GameState(Graphics graphics) {
			Graphics = graphics;
		}
	}
}