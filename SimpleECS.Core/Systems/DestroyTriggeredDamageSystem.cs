using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;

namespace SimpleECS.Core.Components {
	public sealed class DestroyTriggeredDamageSystem : EntityComponentSystem<DamageComponent, SendDamageEvent> {
		public override void Update(Entity entity, DamageComponent damage, SendDamageEvent _) {
			if ( !damage.Persistent ) {
				entity.AddComponent<DestroyEvent>();
			}
		}
	}
}