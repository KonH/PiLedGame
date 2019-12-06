using SimpleECS.Core.Common;
using SimpleECS.Core.States;

namespace SoftwareRender.Core.Render {
	public static class PointRenderer {
		public static void DrawScreenPoint(this FrameState frame, int x, int y, Color color) {
			frame.ChangeAt(new Point2D(x, y), color);
		}
	}
}