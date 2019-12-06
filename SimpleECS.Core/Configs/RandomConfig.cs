namespace SimpleECS.Core.Configs {
	public sealed class RandomConfig {
		public readonly int? Seed;

		public RandomConfig(int? seed = null) {
			Seed = seed;
		}
	}
}