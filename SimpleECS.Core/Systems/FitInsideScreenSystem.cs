using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class FitInsideScreenSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, _, pos) in state.Entities.Get<FitInsideScreenComponent, PositionComponent>() ) {
				pos.Point = FitInsideBorders(pos.Point, state.Graphics.Screen);
			}
		}

		static Point2D FitInsideBorders(Point2D point, Screen borders) {
			return new Point2D(Fit(point.X, 0, borders.Width), Fit(point.Y, 0, borders.Height));
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