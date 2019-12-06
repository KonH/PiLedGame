using System.Collections.Generic;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class EditableEntityComponentSystem<T> : ISystem
		where T : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T>();
			using var editor = entities.Edit();
			Update(filteredEntities, editor);
		}

		public abstract void Update(List<(Entity, T)> entities, EntityEditor editor);
	}
}