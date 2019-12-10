using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class LinearMovementComponent : IComponent {
		public Point2D Direction;
		public double  Interval;
		public double  Timer;

		public LinearMovementComponent Init(Point2D direction, double interval) {
			Direction = direction;
			Interval  = interval;
			Timer     = 0.0;
			return this;
		}
	}
}