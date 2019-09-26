using PiLedGame.Entities;

namespace PiLedGame.Events {
	public sealed class CollisionEvent : IEvent {
		public readonly Entity Other;

		public CollisionEvent(Entity other) {
			Other = other;
		}
	}
}