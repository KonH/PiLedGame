using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class AddHealthSystem : ComponentSystem<HealthComponent, AddHealthEvent> {
		public override void Update(ComponentCollection<HealthComponent, AddHealthEvent> components) {
			foreach ( var (health, _) in components ) {
				health.Health++;
			}
		}
	}
}