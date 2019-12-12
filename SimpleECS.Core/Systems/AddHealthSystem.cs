using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class AddHealthSystem : ComponentSystem<HealthComponent, AddHealthEvent> {
		public override void Update(HealthComponent health, AddHealthEvent _) {
			health.Health++;
		}
	}
}