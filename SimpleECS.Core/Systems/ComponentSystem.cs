using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class ComponentSystem<T> : ISystem
		where T : class, IComponent {
		public void Update(EntitySet entities) {
			var c1 = entities.GetComponent<T>();
			Update(c1);
		}

		public abstract void Update(ComponentCollection<T> components);
	}

	public abstract class ComponentSystem<T1, T2> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public void Update(EntitySet entities) {
			var components = entities.GetComponent<T1, T2>();
			Update(components);
		}

		public abstract void Update(ComponentCollection<T1, T2> components);
	}
}