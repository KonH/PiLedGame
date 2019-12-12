using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class ApplyDamageSystem : ComponentSystem<ApplyDamageEvent, HealthComponent> {
		public override void Update(ApplyDamageEvent damage, HealthComponent health) {
			health.Health -= damage.Value;
		}
	}
}