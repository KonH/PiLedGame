using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public abstract class FinishFrameRealTimeSystemBase : IRealTimeSystem {
		public abstract double TotalElapsedSeconds { get; }

		public void Update(GameState state) {
			state.Time.UpdateFrameTime(TotalElapsedSeconds);
		}
	}
}