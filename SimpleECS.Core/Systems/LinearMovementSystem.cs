using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class LinearMovementSystem : ISystem {
		public void Update(EntitySet entities) {
			var deltaTime = entities.GetFirstComponent<TimeState>().DeltaTime;
			foreach ( var (entity, movement) in entities.Get<LinearMovementComponent>() ) {
				movement.Timer += deltaTime;
				if ( movement.Timer > movement.Interval ) {
					movement.Timer -= movement.Interval;
					entity.AddComponent<MovementEvent>().Init(movement.Direction);
				}
			}
		}
	}
}