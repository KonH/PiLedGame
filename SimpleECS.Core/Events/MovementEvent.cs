using SimpleECS.Core.Common;

namespace SimpleECS.Core.Events {
	public sealed class MovementEvent : BaseEvent {
		public Point2D Offset { get; private set; }

		public void Init(Point2D offset) {
			Offset = offset;
		}
	}
}