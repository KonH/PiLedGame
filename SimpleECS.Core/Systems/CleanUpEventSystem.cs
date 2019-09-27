using SimpleECS.Core.State;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class CleanUpEventSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var entity in state.Entities.Get() ) {
				entity.RemoveComponent(c => c is IEvent);
			}
		}
	}
}