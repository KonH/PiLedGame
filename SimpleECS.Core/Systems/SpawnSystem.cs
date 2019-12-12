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

		public override void Update(PositionComponent position, SpawnEvent ev, EntityEditor editor) {
			if ( ev.Request != _config.Request ) {
				return;
			}
			_config.SpawnCallback(editor.AddEntity(), position.Point);
		}
	}
}