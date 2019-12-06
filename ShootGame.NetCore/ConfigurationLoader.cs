using System;
using SimpleECS.ConsoleLayer.Utils;
using ShootGame.Logic;
using SimpleECS.Core.Configs;

namespace ShootGame.NetCore {
	public static class ConfigurationLoader {
		public static Configuration Load(ArgumentSet args) {
			var isReplayRecord = args.Get("recordReplay", false);
			var replayPath = args.Get("replayPath", null);
			var savedReplayRecord =
				((replayPath != null) && !isReplayRecord) ? new InputRecordConfig(replayPath) : null;
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