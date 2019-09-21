using System;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class PlayerMovementSystem : ISystem {
		public void Update(GameState state) {
			if ( state.Input.Current is ConsoleKey key ) {
				Move(state.Graphics.Screen, state.Entities, key);
			}
		}

		void Move(Screen borders, EntitySet entities, ConsoleKey key) {
			foreach ( var (_, position, _) in entities.Get<PositionComponent, KeyboardControlComponent>() ) {
				position.Point = Move(borders, position.Point, key);
			}
		}

		Point2D Move(Screen borders, Point2D point, ConsoleKey key) {
			return FitInsideBorders(point + Direction(key), borders);
		}

		static Point2D Direction(ConsoleKey key) {
			switch ( key ) {
				case ConsoleKey.LeftArrow: return new Point2D(-1, 0);
				case ConsoleKey.RightArrow: return new Point2D(1, 0);
			}
			return default;
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