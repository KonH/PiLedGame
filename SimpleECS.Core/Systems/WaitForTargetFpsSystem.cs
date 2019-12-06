using System.Threading;
using SimpleECS.Core.Configs;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class WaitForTargetFpsSystem : SingleComponentSystem<TimeState> {
		readonly WaitForTargetFpsConfig _config;

		public WaitForTargetFpsSystem(WaitForTargetFpsConfig config) {
			_config = config;
		}

		public override void Update(TimeState time) {
			var dt = time.UnscaledDeltaTime;
			var lag = _config.TargetTime - dt;
			if ( lag > 0 ) {
				Thread.Sleep((int)(lag / 1000));
			}
		}
	}
}