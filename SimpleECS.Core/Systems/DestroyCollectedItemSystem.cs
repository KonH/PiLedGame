using SimpleECS.Core.State;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class DestroyCollectedItemSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, _) in state.Entities.Get<CollectItemEvent>() ) {
				entity.AddComponent(new DestroyEvent());
			}
		}
	}
}