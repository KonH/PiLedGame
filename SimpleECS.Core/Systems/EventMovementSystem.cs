using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class EventMovementSystem : ComponentSystem<PositionComponent, MovementEvent> {
		public override void Update(PositionComponent position, MovementEvent ev) {
			position.Point += ev.Offset;
		}
	}
}