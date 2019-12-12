using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class DestroySystem : EditableEntityComponentSystem<DestroyEvent> {
		public override void Update(Entity entity, DestroyEvent _, EntityEditor editor) {
			editor.RemoveEntity(entity);
		}
	}
}