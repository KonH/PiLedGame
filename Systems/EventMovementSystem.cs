using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class EventMovementSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, position, ev) in state.Entities.Get<PositionComponent, MovementEvent>() ) {
				position.Point += ev.Offset;
				entity.RemoveComponent(ev);
			}
		}
	}
}