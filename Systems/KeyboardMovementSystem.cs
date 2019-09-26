using System;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class KeyboardMovementSystem : ISystem {
		readonly Func<ConsoleKey, Point2D> _movement;

		public KeyboardMovementSystem(Func<ConsoleKey, Point2D> movement) {
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