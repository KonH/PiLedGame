using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class DestroyCollectedItemSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, _) in state.Entities.Get<CollectItemEvent>() ) {
				entity.AddComponent(new DestroyEvent());
			}
		}
	}
}