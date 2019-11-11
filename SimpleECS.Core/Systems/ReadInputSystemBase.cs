using SimpleECS.Core.Common;
using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public abstract class ReadInputSystemBase : IReadInputSystem {
		public void Update(GameState state) {
			var (isAvailable, keyCode) = TryReadKey();
			if ( isAvailable ) {
				state.Input.Assign(keyCode);
			}
		}

		public abstract (bool, KeyCode) TryReadKey();
	}
}