using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SpawnByEventSystem<T> : ISystem where T : class, IEvent {
		readonly string _requestId;

		public SpawnByEventSystem(string requestId) {
			_requestId = requestId;
		}

		public void Update(GameState state) {
			var shouldTrigger = state.Entities.Get<T>().Count > 0;
			if ( !shouldTrigger ) {
				return;
			}
			foreach ( var (entity, spawn) in state.Entities.Get<SpawnComponent>() ) {
				if ( spawn.RequestId == _requestId ) {
					entity.AddComponent(new SpawnEvent(_requestId));
				}
			}
		}
	}
}