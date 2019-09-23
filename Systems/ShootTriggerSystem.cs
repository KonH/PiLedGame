using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ShootTriggerSystem : ISystem {
		public void Update(GameState state) {
			var key = state.Input.Current;
			foreach ( var (_, trigger, keyboard) in state.Entities.Get<SpawnComponent, KeyboardSpawnComponent>() ) {
				if ( keyboard.Trigger != key ) {
					continue;
				}
				trigger.ShouldSpawn = true;
			}
		}
	}
}