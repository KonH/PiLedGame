using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class EventMovementSystem : ComponentSystem<PositionComponent, MovementEvent> {
		public override void Update(ComponentCollection<PositionComponent, MovementEvent> entities) {
			foreach ( var (position, ev) in entities ) {
				position.Point += ev.Offset;
			}
		}
	}
}