using SimpleECS.Core.Configs;

namespace ShootGame.Logic {
	public sealed class Configuration {
		public readonly InputRecordConfig SavedReplayRecord;
		public readonly bool              IsReplayRecord;
		public readonly string            NewReplayPath;
		public readonly int               RandomSeed;
		public readonly double            FixedFrameTime;

		public bool IsPlaying => !IsReplayShow;
		public bool IsReplayShow => (SavedReplayRecord != null);

		public Configuration(bool isReplayRecord, InputRecordConfig savedReplayRecord, string replayPath, int randomSeed, double fixedFrameTiem) {
			IsReplayRecord    = isReplayRecord;
			SavedReplayRecord = savedReplayRecord;
			NewReplayPath     = replayPath;
			RandomSeed        = randomSeed;
			FixedFrameTime    = fixedFrameTiem;
		}
	}
}