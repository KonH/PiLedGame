using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class EditableComponentSystem<T1, T2> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredComponents = entities.GetComponent<T1, T2>();
			using var editor = entities.Edit();
			foreach ( var (c1, c2) in filteredComponents ) {
				Update(c1, c2, editor);
			}
		}

		public abstract void Update(T1 component1, T2 component2, EntityEditor editor);
	}

	public abstract class EditableComponentSystem<T1, T2, T3> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent
		where T3 : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredComponents = entities.GetComponent<T1, T2, T3>();
			using var editor = entities.Edit();
			foreach ( var (c1, c2, c3) in filteredComponents ) {
				Update(c1, c2, c3, editor);
			}
		}

		public abstract void Update(T1 component1, T2 component2, T3 component3, EntityEditor editor);
	}
}