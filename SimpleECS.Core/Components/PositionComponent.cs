using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public class PositionComponent : IComponent {
		public Point2D Point;

		public PositionComponent(Point2D point) {
			Point = point;
		}
	}
}