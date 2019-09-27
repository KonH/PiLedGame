using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Events {
	public sealed class CollisionEvent : IEvent {
		public readonly Entity Other;

		public CollisionEvent(Entity other) {
			Other = other;
		}
	}
}