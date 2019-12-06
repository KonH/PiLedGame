using System.Collections.Generic;
using SimpleECS.Core.Common;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public abstract class BaseReadInputSystem : ComponentSystem<InputState> {
		public override void Update(List<InputState> inputs) {
			var (isAvailable, keyCode) = TryReadKey();
			if ( !isAvailable ) {
				return;
			}
			foreach ( var input in inputs ) {
				input.Assign(keyCode);
			}
		}

		public abstract (bool, KeyCode) TryReadKey();
	}
}