using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class ApplyDamageSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, damage, health) in state.Entities.Get<ApplyDamageEvent, HealthComponent>() ) {
				health.Health -= damage.Value;
			}
		}
	}
}