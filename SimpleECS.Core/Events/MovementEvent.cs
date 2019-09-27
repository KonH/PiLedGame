using SimpleECS.Core.Common;

namespace SimpleECS.Core.Events {
	public sealed class MovementEvent : IEvent {
		public readonly Point2D Offset;

		public MovementEvent(Point2D offset) {
			Offset = offset;
		}
	}
}