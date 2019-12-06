using System.Collections.Generic;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class ApplyDamageSystem : ComponentSystem<ApplyDamageEvent, HealthComponent> {
		public override void Update(List<(ApplyDamageEvent, HealthComponent)> components) {
			foreach ( var (damage, health) in components ) {
				health.Health -= damage.Value;
			}
		}
	}
}