using System.Collections.Generic;
using SimpleECS.Core.Common;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;

namespace SimpleECS.Core.Systems {
	public sealed class FitInsideScreenSystem : ComponentSystem<FitInsideScreenComponent, PositionComponent> {
		readonly ScreenConfig _screen;

		public FitInsideScreenSystem(ScreenConfig screen) {
			_screen = screen;
		}

		public override void Update(List<(FitInsideScreenComponent, PositionComponent)> components) {
			foreach ( var (_, pos) in components ) {
				pos.Point = FitInsideBorders(pos.Point, _screen);
			}
		}

		static Point2D FitInsideBorders(Point2D point, ScreenConfig screen) {
			return new Point2D(Fit(point.X, 0, screen.Width), Fit(point.Y, 0, screen.Height));
		}

		static int Fit(int value, int min, int max) {
			if ( value >= max ) {
				return min;
			}
			if ( value < min ) {
				return max - 1;
			}
			return value;
		}
	}
}