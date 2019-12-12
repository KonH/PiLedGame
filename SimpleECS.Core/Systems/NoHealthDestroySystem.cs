using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class NoHealthDestroySystem : EntityComponentSystem<HealthComponent> {
		public override void Update(Entity entity, HealthComponent health) {
			if ( health.Health <= 0 ) {
				entity.AddComponent<DestroyEvent>();
			}
		}
	}
}