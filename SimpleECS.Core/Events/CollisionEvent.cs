using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Events {
	public sealed class CollisionEvent : BaseEvent {
		public Entity Other { get; private set; }

		public void Init(Entity other) {
			Other = other;
		}
	}
}