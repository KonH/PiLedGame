using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ResetInputSystem : ISystem {
		public void Update(GameState state) {
			state.Input.Reset();
		}
	}
}