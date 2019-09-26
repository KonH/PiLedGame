using System.Collections.Generic;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class FixedInputSystem : ISystem {
		readonly Queue<InputFrame> _frames;

		public FixedInputSystem(InputRecord record) {
			_frames = new Queue<InputFrame>(record.Frames);
		}

		public void Update(GameState state) {
			var time = state.Time.UnscaledTotalTime;
			if ( _frames.Count == 0 ) {
				return;
			}
			var frame = _frames.Peek();
			if ( time > frame.Time ) {
				state.Input.Assign(frame.Key);
				_frames.Dequeue();
			}
		}
	}
}