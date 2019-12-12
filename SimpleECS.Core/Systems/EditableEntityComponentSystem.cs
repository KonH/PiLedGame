using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class EditableEntityComponentSystem<T> : ISystem
		where T : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T>();
			using var editor = entities.Edit();
			foreach ( var (e, c) in filteredEntities ) {
				Update(e, c, editor);
			}
		}

		public abstract void Update(Entity entity, T component, EntityEditor editor);
	}
}