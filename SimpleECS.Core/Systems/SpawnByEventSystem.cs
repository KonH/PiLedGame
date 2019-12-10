using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class SpawnByEventSystem<T> : ISystem where T : BaseEvent {
		readonly SpawnByEventConfig _config;

		public SpawnByEventSystem(SpawnByEventConfig config) {
			_config = config;
		}

		public void Update(EntitySet entities) {
			var shouldTrigger = entities.Get<T>().Count > 0;
			if ( !shouldTrigger ) {
				return;
			}
			var request = _config.Request;
			foreach ( var (entity, spawn) in entities.Get<SpawnComponent>() ) {
				if ( spawn.Request == request ) {
					entity.AddComponent<SpawnEvent>().Init(request);
				}
			}
		}
	}
}