using System.Collections.Generic;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public abstract class BaseFinishFrameRealTimeSystem : ComponentSystem<TimeState> {
		public abstract double TotalElapsedSeconds { get; }

		public override void Update(List<TimeState> components) {
			foreach ( var time in components ) {
				time.UpdateFrameTime(TotalElapsedSeconds);
			}
		}
	}
}