using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class SendDamageSystem : EntityComponentSystem<DamageComponent, CollisionEvent> {
		public override void Update(Entity entity, DamageComponent damage, CollisionEvent collision) {
			var health = collision.Other.GetComponent<HealthComponent>();
			if ( health == null ) {
				return;
			}
			if ( damage.Layer == health.Layer ) {
				return;
			}
			entity.AddComponent<SendDamageEvent>();
			collision.Other.AddComponent<ApplyDamageEvent>().Init(damage.Damage);
		}
	}
}