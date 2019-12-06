using System.Collections.Generic;
using SimpleECS.Core.Common;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class FixedInputSystem : ISystem {
		readonly Queue<FrameRecord> _frames2;

		public FixedInputSystem(InputRecordConfig config) {
			_frames2 = new Queue<FrameRecord>(config.Frames);
		}

		public void Update(EntitySet entities) {
			var time = entities.GetFirstComponent<TimeState>().UnscaledTotalTime;
			if ( _frames2.Count == 0 ) {
				return;
			}
			var frame = _frames2.Peek();
			if ( time > frame.Time ) {
				foreach ( var (_, input) in entities.Get<InputState>() ) {
					input.Assign(frame.Key);
				}
				_frames2.Dequeue();
			}
		}
	}
}