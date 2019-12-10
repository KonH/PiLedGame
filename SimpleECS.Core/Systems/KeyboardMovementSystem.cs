using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class KeyboardMovementSystem : ISystem {
		readonly KeyboardMovementConfig _config;

		public KeyboardMovementSystem(KeyboardMovementConfig config) {
			_config = config;
		}

		public void Update(EntitySet entities) {
			var input = entities.GetFirstComponent<InputState>().Current;
			foreach ( var (entity, _) in entities.Get<KeyboardMovementComponent>() ) {
				var offset = _config.KeyCodeToOffset(input);
				if ( !offset.IsEmpty ) {
					entity.AddComponent<MovementEvent>().Init(offset);
				}
			}
		}
	}
}