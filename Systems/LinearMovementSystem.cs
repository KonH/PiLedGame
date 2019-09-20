using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class LinearMovementSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var entity in state.Entities.All ) {
				var movement = entity.GetComponent<LinearMovementComponent>();
				if ( movement == null ) {
					continue;
				}
				var position = entity.GetComponent<PositionComponent>();
				if ( position == null ) {
					continue;
				}
				movement.Timer += state.Time.DeltaTime;
				if ( movement.Timer > movement.Interval ) {
					movement.Timer -= movement.Interval;
					position.Point += movement.Direction;
				}
			}
		}
	}
}