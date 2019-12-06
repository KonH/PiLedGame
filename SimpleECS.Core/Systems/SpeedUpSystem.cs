using SimpleECS.Core.Configs;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class SpeedUpSystem : SingleComponentSystem<TimeState> {
		readonly SpeedUpConfig _config;

		double _lastTime;

		public SpeedUpSystem(SpeedUpConfig config) {
			_config = config;
		}

		public override void Update(TimeState time) {
			var totalTime = time.UnscaledTotalTime;
			if ( (_lastTime + _config.Interval) > totalTime ) {
				return;
			}
			_lastTime = totalTime;
			time.TimeScale += _config.Advance;
		}
	}
}