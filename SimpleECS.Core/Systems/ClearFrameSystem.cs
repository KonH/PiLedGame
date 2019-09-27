using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public sealed class ClearFrameSystem : ISystem {
		public void Update(GameState state) {
			state.Graphics.Frame.Clear();
		}
	}
}