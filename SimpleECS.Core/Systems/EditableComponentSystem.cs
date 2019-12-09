using System.Collections.Generic;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class EditableComponentSystem<T1, T2> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredComponents = entities.GetComponent<T1, T2>();
			using var editor = entities.Edit();
			Update(filteredComponents, editor);
		}

		public abstract void Update(List<(T1, T2)> entities, EntityEditor editor);
	}
}