using System.Collections.Generic;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class AddHealthSystem : ComponentSystem<HealthComponent, AddHealthEvent> {
		public override void Update(List<(HealthComponent, AddHealthEvent)> components) {
			foreach ( var (health, _) in components ) {
				health.Health++;
			}
		}
	}
}