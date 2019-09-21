using System;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ShootTriggerSystem : ISystem {
		public void Update(GameState state) {
			var key = state.Input.Current;
			var direction = GetDirection(key);
			if ( direction.IsEmpty ) {
				return;
			}
			foreach ( var (_, trigger, _) in state.Entities.Get<SpawnComponent, KeyboardControlComponent>() ) {
				trigger.ShouldSpawn = true;
				trigger.Direction = direction;
			}
		}

		Point2D GetDirection(ConsoleKey key) {
			switch ( key ) {
				case ConsoleKey.Spacebar: return new Point2D(0, -1);
				default:                  return new Point2D(0, 0);
			}
		}
	}
}