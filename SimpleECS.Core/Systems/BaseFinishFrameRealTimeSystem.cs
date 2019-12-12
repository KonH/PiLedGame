using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public abstract class BaseFinishFrameRealTimeSystem : ComponentSystem<TimeState> {
		public abstract double TotalElapsedSeconds { get; }

		public override void Update(TimeState time) {
			time.UpdateFrameTime(TotalElapsedSeconds);
		}
	}
}