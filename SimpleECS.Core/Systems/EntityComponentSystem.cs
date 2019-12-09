using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class EntityComponentSystem<T> : ISystem
		where T : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T>();
			Update(filteredEntities);
		}

		public abstract void Update(EntityComponentCollection<T> entities);
	}

	public abstract class EntityComponentSystem<T1, T2> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T1, T2>();
			Update(filteredEntities);
		}

		public abstract void Update(EntityComponentCollection<T1, T2> entities);
	}

	public abstract class EntityComponentSystem<T1, T2, T3> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent
		where T3 : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T1, T2, T3>();
			Update(filteredEntities);
		}

		public abstract void Update(EntityComponentCollection<T1, T2, T3> entities);
	}
}