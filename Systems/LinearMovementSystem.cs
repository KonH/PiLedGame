using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class LinearMovementSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, movement) in state.Entities.Get<LinearMovementComponent>() ) {
				movement.Timer += state.Time.DeltaTime;
				if ( movement.Timer > movement.Interval ) {
					movement.Timer -= movement.Interval;
					entity.AddComponent(new MovementEvent(movement.Direction));
				}
			}
		}
	}
}