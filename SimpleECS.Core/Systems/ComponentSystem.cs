using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class ComponentSystem<T> : ISystem
		where T : class, IComponent {
		public void Update(EntitySet entities) {
			var c1 = entities.GetComponent<T>();
			foreach ( var c in c1 ) {
				Update(c);
			}
		}

		public abstract void Update(T component);
	}

	public abstract class ComponentSystem<T1, T2> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public void Update(EntitySet entities) {
			var components = entities.GetComponent<T1, T2>();
			foreach ( var (c1, c2) in components ) {
				Update(c1, c2);
			}
		}

		public abstract void Update(T1 component1, T2 component2);
	}
}