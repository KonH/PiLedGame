using System.Diagnostics;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class FinishFrameRealTimeSystem : ISystem {
		readonly Stopwatch _timer;

		public FinishFrameRealTimeSystem(Stopwatch timer) {
			_timer = timer;
		}

		public void Update(GameState state) {
			state.Time.UpdateFrameTime(_timer.Elapsed.TotalSeconds);
		}
	}
}