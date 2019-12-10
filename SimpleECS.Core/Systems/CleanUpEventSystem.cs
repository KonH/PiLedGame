using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class CleanUpEventSystem : ISystem {
		public void Update(EntitySet entities) {
			foreach ( var entity in entities.GetAll() ) {
				IEvent ev;
				while (true) {
					ev = entity.GetComponent<IEvent>();
					if ( ev == null ) {
						break;
					}
					entity.RemoveComponent(ev);
				}
			}
		}
	}
}