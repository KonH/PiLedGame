using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ConsoleTriggerSystem : IndependentSingleComponentSystem<DebugState, TimeState> {
		readonly DebugConfig _debug;

		double _lastTime = double.MinValue;

		public ConsoleTriggerSystem(DebugConfig debug) {
			_debug = debug;
		}

		public override void Update(DebugState debug, TimeState time) {
			debug.IsTriggered = ShouldTrigger(time);
		}

		bool ShouldTrigger(TimeState time) {
			var nowTime = time.TotalTime;
			if ( nowTime > (_lastTime + _debug.UpdateTime) ) {
				_lastTime = nowTime;
				return true;
			}
			return false;
		}
	}
}