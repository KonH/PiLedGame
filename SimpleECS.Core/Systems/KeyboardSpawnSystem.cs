using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class KeyboardSpawnSystem : ISystem {
		readonly KeyboardSpawnConfig _config;

		public KeyboardSpawnSystem(KeyboardSpawnConfig config) {
			_config = config;
		}

		public void Update(EntitySet entities) {
			foreach ( var input in entities.GetComponent<InputState>() ) {
				var key = input.Current;
				foreach ( var (entity, spawn, _) in entities.Get<SpawnComponent, KeyboardSpawnComponent>() ) {
					if ( (key == _config.Key) && (spawn.Request == _config.Request) ) {
						entity.AddComponent(new SpawnEvent(spawn.Request));
					}
				}
			}
		}
	}
}