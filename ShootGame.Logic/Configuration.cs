using SimpleECS.Core.State;

namespace ShootGame.Logic {
	public sealed class Configuration {
		public readonly InputRecord SavedReplayRecord;
		public readonly InputRecord NewReplayRecord;
		public readonly bool        IsReplayRecord;
		public readonly string      NewReplayPath;
		public readonly int         RandomSeed;

		public bool IsPlaying => !IsReplayShow;
		public bool IsReplayShow => (SavedReplayRecord != null);

		public Configuration(bool isReplayRecord, InputRecord savedReplayRecord, string replayPath, int randomSeed) {
			IsReplayRecord    = isReplayRecord;
			SavedReplayRecord = savedReplayRecord;
			NewReplayPath     = replayPath;
			RandomSeed        = randomSeed;
			if ( IsReplayRecord ) {
				NewReplayRecord = new InputRecord();
			}
		}
	}
}