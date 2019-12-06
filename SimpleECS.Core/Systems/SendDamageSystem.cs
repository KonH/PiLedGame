using System.Collections.Generic;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class SendDamageSystem : EntityComponentSystem<DamageComponent, CollisionEvent> {
		public override void Update(List<(Entity, DamageComponent, CollisionEvent)> entities) {
			foreach ( var (entity, damage, collision) in entities ) {
				var health = collision.Other.GetComponent<HealthComponent>();
				if ( health == null ) {
					continue;
				}
				if ( damage.Layer == health.Layer ) {
					continue;
				}
				entity.AddComponent(new SendDamageEvent());
				collision.Other.AddComponent(new ApplyDamageEvent(damage.Damage));
			}
		}
	}
}