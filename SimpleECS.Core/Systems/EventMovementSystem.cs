using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class EventMovementSystem : EntityComponentSystem<PositionComponent, MovementEvent> {
		public override void Update(EntityComponentCollection<PositionComponent, MovementEvent> entities) {
			foreach ( var (entity, position, ev) in entities ) {
				position.Point += ev.Offset;
				entity.RemoveComponent(ev);
			}
		}
	}
}