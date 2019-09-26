namespace PiLedGame.Components {
	public sealed class TimerComponent : IComponent {
		public double Time;
		public double Interval;

		public TimerComponent(double interval) {
			Interval = interval;
		}
	}
}