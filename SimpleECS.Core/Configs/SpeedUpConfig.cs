namespace SimpleECS.Core.Configs {
	public sealed class SpeedUpConfig {
		public readonly double Interval;
		public readonly double Advance;

		public SpeedUpConfig(double interval, double advance) {
			Interval = interval;
			Advance  = advance;
		}
	}
}