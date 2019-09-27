using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class EventMovementSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, position, ev) in state.Entities.Get<PositionComponent, MovementEvent>() ) {
				position.Point += ev.Offset;
				entity.RemoveComponent(ev);
			}
		}
	}
}