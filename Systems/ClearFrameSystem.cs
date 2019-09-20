using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ClearFrameSystem : ISystem {
		public void Update(GameState state) {
			state.Graphics.Frame.Clear();
		}
	}
}