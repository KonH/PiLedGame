using System.Collections.Generic;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;

namespace SimpleECS.Core.Components {
	public sealed class DestroyTriggeredDamageSystem : EntityComponentSystem<DamageComponent, SendDamageEvent> {
		public override void Update(List<(Entity, DamageComponent, SendDamageEvent)> entities) {
			foreach ( var (entity, damage, _) in entities ) {
				if ( !damage.Persistent ) {
					entity.AddComponent(new DestroyEvent());
				}
			}
		}
	}
}