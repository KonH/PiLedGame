using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class PreventSpawnCollisionSystem : EntityComponentSystem<SpawnEvent, CollisionEvent> {
		readonly PreventSpawnCollisionConfig _config;

		public PreventSpawnCollisionSystem(PreventSpawnCollisionConfig config) {
			_config = config;
		}

		public override void Update(Entity entity, SpawnEvent ev, CollisionEvent _) {
			if ( _config.Requests.Contains(ev.Request) ) {
				entity.RemoveComponent(ev);
			}
		}
	}
}