using System.Collections.Generic;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class ResetInputSystem : ComponentSystem<InputState> {
		public override void Update(List<InputState> components) {
			foreach ( var input in components ) {
				input.Reset();
			}
		}
	}
}