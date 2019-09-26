using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ApplyDamageSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, damage, health) in state.Entities.Get<ApplyDamageEvent, HealthComponent>() ) {
				health.Health -= damage.Value;
			}
		}
	}
}