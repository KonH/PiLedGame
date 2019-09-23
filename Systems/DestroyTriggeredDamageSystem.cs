using PiLedGame.State;
using PiLedGame.Systems;

namespace PiLedGame.Components {
	public sealed class DestroyTriggeredDamageSystem : ISystem {
		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (e, damage) in state.Entities.Get<DamageComponent>() ) {
					if ( damage.Triggered && !damage.Persistent ) {
						editor.RemoveEntity(e);
					}
				}
			}
		}
	}
}