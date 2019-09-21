using System.Threading;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class WaitForTargetFpsSystem : ISystem {
		readonly double _targetTime;

		public WaitForTargetFpsSystem(int targetFps) {
			_targetTime = (double)1000 / targetFps;
		}

		public void Update(GameState state) {
			var dt = state.Time.DeltaTime;
			var lag = _targetTime - dt;
			if ( lag > 0 ) {
				Thread.Sleep((int)(lag / 1000));
			}
		}
	}
}