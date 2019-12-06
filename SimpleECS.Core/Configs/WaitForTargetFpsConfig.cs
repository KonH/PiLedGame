namespace SimpleECS.Core.Configs {
	public sealed class WaitForTargetFpsConfig {
		public readonly double TargetTime;

		public WaitForTargetFpsConfig(int targetFps) {
			TargetTime = (double) 1000 / targetFps;
		}
	}
}