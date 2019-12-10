namespace SimpleECS.Core.Components {
	public sealed class RandomSpawnComponent : IComponent {
		public double MinInterval { get; private set; }
		public double MaxInterval { get; private set; }

		public RandomSpawnComponent Init(double minInterval, double maxInterval) {
			MinInterval = minInterval;
			MaxInterval = maxInterval;
			return this;
		}
	}
}