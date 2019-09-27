using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class SendDamageSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, damage, collision) in state.Entities.Get<DamageComponent, CollisionEvent>() ) {
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