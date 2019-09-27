using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public sealed class ResetInputSystem : ISystem {
		public void Update(GameState state) {
			state.Input.Reset();
		}
	}
}