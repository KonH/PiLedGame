using System.Collections.Generic;
using SimpleECS.Core.Configs;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class FinishFrameFixedTimeSystem : ComponentSystem<TimeState> {
		readonly FinishFrameFixedTimeConfig _config;

		double _accum;

		public FinishFrameFixedTimeSystem(FinishFrameFixedTimeConfig config) {
			_config = config;
		}

		public override void Update(List<TimeState> components) {
			_accum += _config.Interval;
			foreach ( var time in components ) {
				time.UpdateFrameTime(_accum);
			}
		}
	}
}