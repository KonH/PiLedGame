namespace SimpleECS.Core.Components {
	public sealed class TimerComponent : IComponent {
		public double Time;
		public double Interval { get; private set; }

		public void Init(double interval) {
			Time = 0.0;
			Interval = interval;
		}
	}
}