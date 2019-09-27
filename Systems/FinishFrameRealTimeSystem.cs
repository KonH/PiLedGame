using System.Diagnostics;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class FinishFrameRealTimeSystem : ISystem, IInit {
		readonly Stopwatch _timer = new Stopwatch();

		public void Init(GameState state) {
			_timer.Start();
		}

		public void Update(GameState state) {
			state.Time.UpdateFrameTime(_timer.Elapsed.TotalSeconds);
		}
	}
}