namespace SimpleECS.Core.Components {
	public sealed class RandomSpawnComponent : IComponent {
		public readonly double MinInterval;
		public readonly double MaxInterval;

		public RandomSpawnComponent(double minInterval, double maxInterval) {
			MinInterval = minInterval;
			MaxInterval = maxInterval;
		}
	}
}