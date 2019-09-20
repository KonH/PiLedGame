using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class LinearMovementSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, movement, position) in state.Entities.Get<LinearMovementComponent, PositionComponent>() ) {
				movement.Timer += state.Time.DeltaTime;
				if ( movement.Timer > movement.Interval ) {
					movement.Timer -= movement.Interval;
					position.Point += movement.Direction;
				}
			}
		}
	}
}