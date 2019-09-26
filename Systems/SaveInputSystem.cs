using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SaveInputSystem : ISystem {
		readonly InputRecord _record;

		public SaveInputSystem(InputRecord record) {
			_record = record;
		}

		public void Update(GameState state) {
			var key = state.Input.Current;
			if ( key != default ) {
				_record.Frames.Add(new InputFrame(state.Time.UnscaledTotalTime, key));
			}
		}
	}
}