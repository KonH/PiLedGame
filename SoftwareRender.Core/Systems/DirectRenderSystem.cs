using SimpleECS.Core.Common;
using SimpleECS.Core.State;
using SimpleECS.Core.Systems;

namespace SoftwareRender.Core.Systems {
	public sealed class DirectRenderSystem : ISystem {
		public void Update(GameState state) {
			state.Graphics.Frame.ChangeAt(new Point2D(16, 16), Color.Red);
		}
	}
}