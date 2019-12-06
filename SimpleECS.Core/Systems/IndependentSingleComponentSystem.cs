using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class IndependentSingleComponentSystem<T1, T2> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public void Update(EntitySet entities) {
			var c1 = entities.GetFirstComponent<T1>();
			var c2 = entities.GetFirstComponent<T2>();
			Update(c1, c2);
		}

		public abstract void Update(T1 component1, T2 component2);
	}
}