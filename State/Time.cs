using System.Diagnostics;

namespace PiLedGame.State {
	public sealed class Time {
		public double UnscaledTotalTime { get; private set; }
		public double TotalTime         { get; private set; }
		public double UnscaledDeltaTime { get; private set; }
		public double DeltaTime         { get; private set; }
		public double TimeScale         { get; set; } = 1.0;

		Stopwatch _frameTimer = null;

		public Time() {
			_frameTimer = Stopwatch.StartNew();
		}

		public void UpdateFrameTime() {
			var prevTotalTime = UnscaledTotalTime;
			UnscaledTotalTime = _frameTimer.Elapsed.TotalSeconds;
			UnscaledDeltaTime = (UnscaledTotalTime - prevTotalTime);
			DeltaTime = UnscaledDeltaTime * TimeScale;
			TotalTime += DeltaTime;
		}
	}
}