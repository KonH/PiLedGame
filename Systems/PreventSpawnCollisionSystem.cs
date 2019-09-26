using System.Linq;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class PreventSpawnCollisionSystem : ISystem {
		readonly string[] _requestIds;

		public PreventSpawnCollisionSystem(params string[] requestIds) {
			_requestIds = requestIds;
		}

		public void Update(GameState state) {
			foreach ( var (entity, ev, _) in state.Entities.Get<SpawnEvent, CollisionEvent>() ) {
				if ( _requestIds.Contains(ev.RequestId) ) {
					entity.RemoveComponent(ev);
				}
			}
		}
	}
}