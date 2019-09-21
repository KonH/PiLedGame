namespace PiLedGame.Components {
	public sealed class RandomSpawnComponent : IComponent {
		public readonly double MinInterval;
		public readonly double MaxInterval;

		public double Interval;
		public double Timer;

		public RandomSpawnComponent(double minInterval, double maxInterval) {
			MinInterval = minInterval;
			MaxInterval = maxInterval;
		}
	}
}