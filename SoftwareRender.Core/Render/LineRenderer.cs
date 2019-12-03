using System;
using SimpleECS.Core.Common;
using SimpleECS.Core.State;

namespace SoftwareRender.Core.Render {
	public static class LineRenderer {
		public static void DrawScreenLine(this Frame frame, int x0, int y0, int x1, int y1, Color color) {
			var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if ( steep ) {
				Common.Swap(ref x0, ref y0);
				Common.Swap(ref x1, ref y1);
			}
			if ( x0 > x1 ) {
				Common.Swap(ref x0, ref x1);
				Common.Swap(ref y0, ref y1);
			}
			var dx = x1 - x0;
			var dy = Math.Abs(y1 - y0);
			var error = dx / 2;
			var ystep = (y0 < y1) ? 1 : -1;
			var y = y0;
			for ( var x = x0; x <= x1; x++ ) {
				frame.DrawScreenPoint(steep ? y : x, steep ? x : y, color);
				error -= dy;
				if ( error < 0 ) {
					y += ystep;
					error += dx;
				}
			}
		}

		public static void DrawScreenLine(this Frame frame, Point2D p0, Point2D p1, Color color) {
			frame.DrawScreenLine(p0.X, p0.Y, p1.X, p1.Y, color);
		}
	}
}