using System;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.State {
	public sealed class GameState {
		public Graphics  Graphics  { get; }
		public Debug     Debug     { get; }
		public Random    Random    { get; }
		public Execution Execution { get; } = new Execution();
		public Input     Input     { get; } = new Input();
		public Time      Time      { get; } = new Time();
		public EntitySet Entities  { get; } = new EntitySet();

		public GameState(Graphics graphics, Debug debug, Random random) {
			Graphics = graphics;
			Debug    = debug;
			Random   = random;
		}
	}
}