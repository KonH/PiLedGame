using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class NoHealthDestroySystem : ISystem {
		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (entity, health) in state.Entities.Get<HealthComponent>() ) {
					if ( health.Health <= 0 ) {
						editor.RemoveEntity(entity);
					}
				}
			}
		}
	}
}