using System.Collections.Generic;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class DestroySystem : EditableEntityComponentSystem<DestroyEvent> {
		public override void Update(List<(Entity, DestroyEvent)> entities, EntityEditor editor) {
			foreach ( var (entity, _) in entities ) {
				editor.RemoveEntity(entity);
			}
		}
	}
}