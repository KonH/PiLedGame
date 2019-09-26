using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class FinishFrameFixedTimeSystem : ISystem {
		readonly double _interval;

		double _accum;

		public FinishFrameFixedTimeSystem(double interval) {
			_interval = interval;
		}

		public void Update(GameState state) {
			_accum += _interval;
			state.Time.UpdateFrameTime(_accum);
		}
	}
}