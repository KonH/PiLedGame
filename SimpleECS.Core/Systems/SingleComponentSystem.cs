using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class SingleComponentSystem<T> : ISystem
		where T : class, IComponent {
		public void Update(EntitySet entities) {
			var c1 = entities.GetFirstComponent<T>();
			Update(c1);
		}

		public abstract void Update(T component);
	}
}