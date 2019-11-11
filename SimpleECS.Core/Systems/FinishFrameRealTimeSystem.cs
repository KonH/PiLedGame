using System.Diagnostics;
using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public sealed class FinishFrameRealTimeSystem : FinishFrameRealTimeSystemBase, IInit {
		readonly Stopwatch _timer = new Stopwatch();

		public override double TotalElapsedSeconds => _timer.Elapsed.TotalSeconds;

		public void Init(GameState state) {
			_timer.Start();
		}
	}
}