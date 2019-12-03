using SimpleECS.Core.Common;
using SimpleECS.Core.State;
using SimpleECS.Core.Systems;
using SoftwareRender.Core.Render;

namespace SoftwareRender.Core.Systems {
	public sealed class DirectRenderSystem : ISystem {
		public void Update(GameState state) {
			var frame = state.Graphics.Frame;
			var points = new[] { new Point3D(5, 5, 5) };
			DrawPoints(frame, points, Color.Red);
		}

		static void DrawPoints(Frame frame, Point3D[] points, Color color) {
			foreach ( var point in points ) {
				var (x, y) = ToScreenPoint(point);
				frame.DrawScreenPoint(x, y, color);
			}
		}

		static Point2D ToScreenPoint(Point3D point) {
			return new Point2D((int)point.X, (int)point.Y);
		}
	}
}