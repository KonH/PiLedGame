using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class CleanUpEventSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var entity in state.Entities.Get() ) {
				entity.RemoveComponent(c => c is IEvent);
			}
		}
	}
}