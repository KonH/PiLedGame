namespace SimpleECS.Core.Configs {
	public sealed class DebugConfig {
		public readonly float UpdateTime;
		public readonly int   MaxLogSize;

		public DebugConfig(float updateTime, int maxLogSize) {
			UpdateTime = updateTime;
			MaxLogSize = maxLogSize;
		}
	}
}