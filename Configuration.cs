using System;
using PiLedGame.State;

namespace PiLedGame {
	public sealed class Configuration {
		public readonly InputRecord SavedReplayRecord;
		public readonly InputRecord NewReplayRecord;
		public readonly bool        IsReplayRecord;
		public readonly string      NewReplayPath;
		public readonly int         RandomSeed;

		public bool IsPlaying => !IsReplayShow;
		public bool IsReplayShow => (SavedReplayRecord != null);

		Configuration(bool isReplayRecord, InputRecord savedReplayRecord, string replayPath, int randomSeed) {
			IsReplayRecord    = isReplayRecord;
			SavedReplayRecord = savedReplayRecord;
			NewReplayPath     = replayPath;
			RandomSeed        = randomSeed;
			if ( IsReplayRecord ) {
				NewReplayRecord = new InputRecord();
			}
		}

		public static Configuration Load(ArgumentSet args) {
			var isReplayRecord = args.Get("recordReplay", false);
			var replayPath = args.Get("replayPath", null);
			var savedReplayRecord =
				((replayPath != null) && !isReplayRecord) ? InputRecord.Load(replayPath) : null;
			var randomSeed = args.Get("randomSeed", 0);
			if ( (isReplayRecord || (replayPath != null)) && (randomSeed == 0) ) {
				throw new InvalidOperationException("Random seed should be set to record/rewind replay!");
			}
			if ( isReplayRecord && (replayPath == null) ) {
				throw new InvalidOperationException("Replay path should be set to record replay!");
			}
			return new Configuration(isReplayRecord, savedReplayRecord, replayPath, randomSeed);
		}
	}
}