using System.Collections.Generic;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class PreventSpawnCollisionSystem : ISystem {
		readonly HashSet<SpawnRequestType> _requests;

		public PreventSpawnCollisionSystem(params SpawnRequestType[] requests) {
			_requests = new HashSet<SpawnRequestType>(requests);
		}

		public void Update(GameState state) {
			foreach ( var (entity, ev, _) in state.Entities.Get<SpawnEvent, CollisionEvent>() ) {
				if ( _requests.Contains(ev.Request) ) {
					entity.RemoveComponent(ev);
				}
			}
		}
	}
}