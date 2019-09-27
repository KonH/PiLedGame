namespace SimpleECS.Core.State {
	public sealed class Time {
		public double UnscaledTotalTime { get; private set; }
		public double TotalTime         { get; private set; }
		public double UnscaledDeltaTime { get; private set; }
		public double DeltaTime         { get; private set; }
		public double TimeScale         { get; set; } = 1.0;

		public void UpdateFrameTime(double totalSeconds) {
			var prevTotalTime = UnscaledTotalTime;
			UnscaledTotalTime = totalSeconds;
			UnscaledDeltaTime = (UnscaledTotalTime - prevTotalTime);
			DeltaTime = UnscaledDeltaTime * TimeScale;
			TotalTime += DeltaTime;
		}
	}
}