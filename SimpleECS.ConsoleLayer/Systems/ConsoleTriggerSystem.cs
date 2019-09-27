using SimpleECS.Core.State;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ConsoleTriggerSystem : ISystem {
		double _lastTime = double.MinValue;

		public void Update(GameState state) {
			state.Debug.IsTriggered = ShouldTrigger(state);
		}

		bool ShouldTrigger(GameState state) {
			var nowTime = state.Time.TotalTime;
			if ( nowTime > (_lastTime + state.Debug.UpdateTime) ) {
				_lastTime = nowTime;
				return true;
			}
			return false;
		}
	}
}