using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class FinishFrameSystem : ISystem {
		public void Update(GameState state) {
			state.Time.UpdateFrameTime();
		}
	}
}