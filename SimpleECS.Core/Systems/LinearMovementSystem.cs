using SimpleECS.Core.Components;
using SimpleECS.Core.Events;
using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
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