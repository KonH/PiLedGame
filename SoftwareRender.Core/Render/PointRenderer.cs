using SimpleECS.Core.Common;
using SimpleECS.Core.State;

namespace SoftwareRender.Core.Render {
	public static class PointRenderer {
		public static void DrawScreenPoint(this Frame frame, int x, int y, Color color) {
			frame.ChangeAt(new Point2D(x, y), color);
		}
	}
}