using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class AddHealthSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, health, _) in state.Entities.Get<HealthComponent, AddHealthEvent>() ) {
				health.Health++;
			}
		}
	}
}