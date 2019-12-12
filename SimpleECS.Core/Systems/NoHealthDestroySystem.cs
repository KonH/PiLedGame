using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class NoHealthDestroySystem : EntityComponentSystem<HealthComponent> {
		public override void Update(EntityComponentCollection<HealthComponent> entities) {
			foreach ( var (e, health) in entities ) {
				if ( health.Health <= 0 ) {
					e.AddComponent<DestroyEvent>();
				}
			}
		}
	}
}