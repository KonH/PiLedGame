using System.Diagnostics;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class FinishFrameRealTimeSystem : BaseFinishFrameRealTimeSystem, IInit {
		readonly Stopwatch _timer = new Stopwatch();

		public override double TotalElapsedSeconds => _timer.Elapsed.TotalSeconds;

		public void Init(EntitySet entities) {
			_timer.Start();
		}
	}
}