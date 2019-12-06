using SimpleECS.Core.Common;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class SaveInputSystem : ISystem {
		public SaveInputSystem() {}

		public void Update(EntitySet entities) {
			var key = entities.GetFirstComponent<InputState>().Current;
			if ( key != default ) {
				var recordState = entities.GetFirstComponent<InputRecordState>();
				var time = entities.GetFirstComponent<TimeState>().UnscaledTotalTime;
				recordState.Frames.Add(new FrameRecord(time, key));
			}
		}
	}
}