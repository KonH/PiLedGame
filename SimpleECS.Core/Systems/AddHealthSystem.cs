using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class AddHealthSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, health, _) in state.Entities.Get<HealthComponent, AddHealthEvent>() ) {
				health.Health++;
			}
		}
	}
}