using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class SpawnByEventSystem<T> : ISystem where T : class, IEvent {
		readonly SpawnRequestType _request;

		public SpawnByEventSystem(SpawnRequestType request) {
			_request = request;
		}

		public void Update(GameState state) {
			var shouldTrigger = state.Entities.Get<T>().Count > 0;
			if ( !shouldTrigger ) {
				return;
			}
			foreach ( var (entity, spawn) in state.Entities.Get<SpawnComponent>() ) {
				if ( spawn.Request == _request ) {
					entity.AddComponent(new SpawnEvent(_request));
				}
			}
		}
	}
}