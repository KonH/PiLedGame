using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class ResetInputSystem : ComponentSystem<InputState> {
		public override void Update(InputState input) {
			input.Reset();
		}
	}
}