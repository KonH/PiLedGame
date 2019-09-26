using PiLedGame.Common;

namespace PiLedGame.Events {
	public sealed class MovementEvent : IEvent {
		public readonly Point2D Offset;

		public MovementEvent(Point2D offset) {
			Offset = offset;
		}
	}
}