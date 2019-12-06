using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class CleanUpEventSystem : ISystem {
		public void Update(EntitySet entities) {
			foreach ( var entity in entities.Get() ) {
				entity.RemoveComponent(c => c is IEvent);
			}
		}
	}
}