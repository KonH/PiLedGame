using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public abstract class EntityComponentSystem<T> : ISystem
		where T : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T>();
			foreach ( var (e, c) in filteredEntities ) {
				Update(e, c);
			}
		}

		public abstract void Update(Entity entity, T component);
	}

	public abstract class EntityComponentSystem<T1, T2> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T1, T2>();
			foreach ( var (e, c1, c2) in filteredEntities ) {
				Update(e, c1, c2);
			}
		}

		public abstract void Update(Entity entity, T1 component1, T2 component2);
	}

	public abstract class EntityComponentSystem<T1, T2, T3> : ISystem
		where T1 : class, IComponent
		where T2 : class, IComponent
		where T3 : class, IComponent {
		public void Update(EntitySet entities) {
			var filteredEntities = entities.Get<T1, T2, T3>();
			foreach ( var (e, c1, c2, c3) in filteredEntities ) {
				Update(e, c1, c2, c3);
			}
		}

		public abstract void Update(Entity entity, T1 component1, T2 component2, T3 component3);
	}
}