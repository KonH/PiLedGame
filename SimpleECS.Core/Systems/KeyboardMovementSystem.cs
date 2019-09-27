using System;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class KeyboardMovementSystem : ISystem {
		readonly Func<KeyCode, Point2D> _movement;

		public KeyboardMovementSystem(Func<KeyCode, Point2D> movement) {
			_movement = movement;
		}

		public void Update(GameState state) {
			foreach ( var (entity, _) in state.Entities.Get<KeyboardMovementComponent>() ) {
				var offset = _movement(state.Input.Current);
				if ( !offset.IsEmpty ) {
					entity.AddComponent(new MovementEvent(offset));
				}
			}
		}
	}
}