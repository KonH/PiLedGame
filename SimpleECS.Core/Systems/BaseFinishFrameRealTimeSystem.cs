using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public abstract class BaseFinishFrameRealTimeSystem : ComponentSystem<TimeState> {
		public abstract double TotalElapsedSeconds { get; }

		public override void Update(ComponentCollection<TimeState> components) {
			foreach ( var time in components ) {
				time.UpdateFrameTime(TotalElapsedSeconds);
			}
		}
	}
}