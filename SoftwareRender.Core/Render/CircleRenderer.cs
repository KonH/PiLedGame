using SimpleECS.Core.Common;
using SimpleECS.Core.State;

namespace SoftwareRender.Core.Render {
	public static class CircleRenderer {
		public static void DrawScreenCircle(this Frame frame, int x0, int y0, int radius, Color color) {
			void DrawPoint(int xn, int yn) {
				frame.DrawScreenPoint(xn, yn, color);
			}

			var x = radius;
			var y = 0;
			var radiusError = 1 - x;
			while ( x >= y ) {
				DrawPoint(x + x0, y + y0);
				DrawPoint(y + x0, x + y0);
				DrawPoint(-x + x0, y + y0);
				DrawPoint(-y + x0, x + y0);
				DrawPoint(-x + x0, -y + y0);
				DrawPoint(-y + x0, -x + y0);
				DrawPoint(x + x0, -y + y0);
				DrawPoint(y + x0, -x + y0);
				y++;
				if ( radiusError < 0 ) {
					radiusError += 2 * y + 1;
				} else {
					x--;
					radiusError += 2 * (y - x + 1);
				}
			}
		}
	}
}