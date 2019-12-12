using SimpleECS.Core.Common;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public abstract class BaseReadInputSystem : ComponentSystem<InputState> {
		public override void Update(InputState input) {
			var (isAvailable, keyCode) = TryReadKey();
			if ( !isAvailable ) {
				return;
			}
			input.Assign(keyCode);
		}

		public abstract (bool, KeyCode) TryReadKey();
	}
}