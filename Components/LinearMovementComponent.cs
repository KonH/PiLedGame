using PiLedGame.Common;

namespace PiLedGame.Components {
	public sealed class LinearMovementComponent : IComponent {
		public Point2D Direction;
		public double  Interval;
		public double  Timer;

		public LinearMovementComponent(Point2D direction, double interval) {
			Direction = direction;
			Interval  = interval;
		}
	}
}