using PiLedGame.Entities;

namespace PiLedGame.State {
	public sealed class GameState {
		public Graphics  Graphics  { get; }
		public Debug     Debug     { get; }
		public Execution Execution { get; } = new Execution();
		public Input     Input     { get; } = new Input();
		public Time      Time      { get; } = new Time();
		public EntitySet Entities  { get; } = new EntitySet();

		public GameState(Graphics graphics, Debug debug) {
			Graphics = graphics;
			Debug    = debug;
		}
	}
}