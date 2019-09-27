using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public sealed class SpeedUpSystem : ISystem {
		readonly double _interval;
		readonly double _advance;

		double _lastTime;

		public SpeedUpSystem(double interval, double advance) {
			_interval = interval;
			_advance  = advance;
		}

		public void Update(GameState state) {
			var time = state.Time.UnscaledTotalTime;
			if ( (_lastTime + _interval) > time ) {
				return;
			}
			_lastTime = time;
			state.Time.TimeScale += _advance;
		}
	}
}