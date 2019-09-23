using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class PlayerMovementSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, position, keyboard) in state.Entities.Get<PositionComponent, KeyboardMovementComponent>() ) {
				var offset = keyboard.Movement(state.Input.Current);
				position.Point = Move(state.Graphics.Screen, position.Point, offset);
			}
		}

		Point2D Move(Screen borders, Point2D point, Point2D offset) {
			return FitInsideBorders(point + offset, borders);
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