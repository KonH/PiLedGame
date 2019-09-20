using PiLedGame.Common;

namespace PiLedGame.Components {
	public class PositionComponent : IComponent {
		public Point2D Point;

		public PositionComponent(Point2D point) {
			Point = point;
		}
	}
}