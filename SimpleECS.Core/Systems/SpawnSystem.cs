using System.Collections.Generic;
using SimpleECS.Core.Events;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;

namespace SimpleECS.Core.Systems {
	public sealed class SpawnSystem : EditableComponentSystem<PositionComponent, SpawnEvent> {
		readonly SpawnConfig _config;

		public SpawnSystem(SpawnConfig config) {
			_config = config;
		}

		public override void Update(ComponentCollection<PositionComponent, SpawnEvent> entities, EntityEditor editor) {
			foreach ( var (position, ev) in entities ) {
				if ( ev.Request != _config.Request ) {
					continue;
				}
				_config.SpawnCallback(editor.AddEntity(), position.Point);
			}
		}
	}
}