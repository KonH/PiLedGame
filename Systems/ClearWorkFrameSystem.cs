using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ClearWorkFrameSystem : ISystem {
		public void Update(GameState state) {
			state.Graphics.WorkFrame.Clear();
		}
	}
}