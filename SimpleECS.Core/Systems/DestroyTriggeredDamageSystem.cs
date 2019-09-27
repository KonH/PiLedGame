using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;

namespace SimpleECS.Core.Components {
	public sealed class DestroyTriggeredDamageSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, damage, _) in state.Entities.Get<DamageComponent, SendDamageEvent>() ) {
				if ( !damage.Persistent ) {
					entity.AddComponent(new DestroyEvent());
				}
			}
		}
	}
}