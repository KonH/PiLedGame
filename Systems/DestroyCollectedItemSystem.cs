using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class DestroyCollectedItemSystem : ISystem {
		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (e, item) in state.Entities.Get<ItemComponent>() ) {
					if ( item.IsCollected ) {
						editor.RemoveEntity(e);
					}
				}
			}
		}
	}
}