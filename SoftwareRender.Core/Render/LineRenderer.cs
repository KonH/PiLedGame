using System;
using SimpleECS.Core.Common;
using SimpleECS.Core.States;
using SoftwareRender.Core.Utils;

namespace SoftwareRender.Core.Render {
	public static class LineRenderer {
		public static void DrawScreenLine(this FrameState frame, int x0, int y0, int x1, int y1, Color color) {
			var steep = NormalizeLine(ref x0, ref y0, ref x1, ref y1);
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

		public static void DrawScreenLineSmooth(this FrameState frame, int x0, int y0, int x1, int y1, Color color) {
			void DrawScreenPoint(bool swap, int vx, int vy, float intensity) {
				if ( swap ) {
					Common.Swap(ref vx, ref vy);
				}
				frame.DrawScreenPoint(vx, vy, color.ChangeAlpha((byte)(intensity * color.A)));
			}

			var steep = NormalizeLine(ref x0, ref y0, ref x1, ref y1);
			DrawScreenPoint(steep, x0, y0, 1);
			DrawScreenPoint(steep, x1, y1, 1);
			var dx = x1 - x0;
			var dy = y1 - y0;
			var gradient = (float)dy / dx;
			var y = y0 + gradient;
			for ( var x = x0 + 1; x < x1; x++ ) {
				DrawScreenPoint(steep, x, (int)y, 1 - (y - (int)y));
				DrawScreenPoint(steep, x, (int)y + 1, y - (int)y);
				y += gradient;
			}
		}

		static bool NormalizeLine(ref int x0, ref int y0, ref int x1, ref int y1) {
			var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if ( steep ) {
				Common.Swap(ref x0, ref y0);
				Common.Swap(ref x1, ref y1);
			}
			if ( x0 > x1 ) {
				Common.Swap(ref x0, ref x1);
				Common.Swap(ref y0, ref y1);
			}
			return steep;
		}
	}
}