using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class ApplyDamageSystem : ComponentSystem<ApplyDamageEvent, HealthComponent> {
		public override void Update(ComponentCollection<ApplyDamageEvent, HealthComponent> components) {
			foreach ( var (damage, health) in components ) {
				health.Health -= damage.Value;
			}
		}
	}
}