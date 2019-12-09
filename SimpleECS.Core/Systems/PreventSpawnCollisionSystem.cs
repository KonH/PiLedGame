using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class PreventSpawnCollisionSystem : EntityComponentSystem<SpawnEvent, CollisionEvent> {
		readonly PreventSpawnCollisionConfig _config;

		public PreventSpawnCollisionSystem(PreventSpawnCollisionConfig config) {
			_config = config;
		}

		public override void Update(EntityComponentCollection<SpawnEvent, CollisionEvent> entities) {
			foreach ( var (entity, ev, _) in entities ) {
				if ( _config.Requests.Contains(ev.Request) ) {
					entity.RemoveComponent(ev);
				}
			}
		}
	}
}