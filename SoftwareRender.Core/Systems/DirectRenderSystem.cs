using SimpleECS.Core.Common;
using SimpleECS.Core.State;
using SimpleECS.Core.Systems;
using SoftwareRender.Core.Render;

namespace SoftwareRender.Core.Systems {
	public sealed class DirectRenderSystem : ISystem {
		public void Update(GameState state) {
			var frame = state.Graphics.Frame;

			frame.DrawScreenPoint(0, 0, Color.Red);
			frame.DrawScreenPoint(31, 0, Color.Red);
			frame.DrawScreenPoint(0, 31, Color.Red);
			frame.DrawScreenPoint(31, 31, Color.Red);

			frame.DrawScreenLine(1, 1, 30, 30, Color.Green);
			frame.DrawScreenLine(30, 1, 1, 30, Color.Green);
			frame.DrawScreenLine(4, 0, 4, 31, Color.Green);
			frame.DrawScreenLine(0, 27, 31, 27, Color.Green);

			frame.DrawScreenLineSmooth(1, 5, 20, 10, Color.Firebrick);

			frame.DrawScreenCircle(20, 20, 8, Color.Indigo);
		}
	}
}