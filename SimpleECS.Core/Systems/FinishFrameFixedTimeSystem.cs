using SimpleECS.Core.Configs;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class FinishFrameFixedTimeSystem : SingleComponentSystem<TimeState> {
		readonly FinishFrameFixedTimeConfig _config;

		double _accum;

		public FinishFrameFixedTimeSystem(FinishFrameFixedTimeConfig config) {
			_config = config;
		}

		public override void Update(TimeState time) {
			_accum += _config.Interval;
			time.UpdateFrameTime(_accum);
		}
	}
}