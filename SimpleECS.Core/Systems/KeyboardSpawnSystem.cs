using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class KeyboardSpawnSystem : ISystem {
		readonly KeyCode          _key;
		readonly SpawnRequestType _request;

		public KeyboardSpawnSystem(KeyCode key, SpawnRequestType request) {
			_key     = key;
			_request = request;
		}

		public void Update(GameState state) {
			var key = state.Input.Current;
			foreach ( var (entity, spawn, _) in state.Entities.Get<SpawnComponent, KeyboardSpawnComponent>() ) {
				if ( (key == _key) && (_request == spawn.Request) ) {
					entity.AddComponent(new SpawnEvent(spawn.Request));
				}
			}
		}
	}
}