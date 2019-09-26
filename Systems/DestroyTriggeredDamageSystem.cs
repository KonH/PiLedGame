using PiLedGame.Events;
using PiLedGame.State;
using PiLedGame.Systems;

namespace PiLedGame.Components {
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