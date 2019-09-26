using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class DestroySystem : ISystem {
		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (entity, _) in state.Entities.Get<DestroyEvent>() ) {
					editor.RemoveEntity(entity);
				}
			}
		}
	}
}