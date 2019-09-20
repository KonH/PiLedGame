using System.Diagnostics;

namespace PiLedGame.State {
	public sealed class Time {
		public double DeltaTime { get; private set; }
		public double TotalTime { get; private set; }

		Stopwatch _frameTimer = null;

		public Time() {
			_frameTimer = Stopwatch.StartNew();
		}

		public void UpdateFrameTime() {
			var prevTotalTime = TotalTime;
			TotalTime = _frameTimer.Elapsed.TotalSeconds;
			DeltaTime = (TotalTime - prevTotalTime);
		}
	}
}