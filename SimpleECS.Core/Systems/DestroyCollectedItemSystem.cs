using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class DestroyCollectedItemSystem : EntityComponentSystem<CollectItemEvent> {
		public override void Update(EntityComponentCollection<CollectItemEvent> entities) {
			foreach ( var (entity, _) in entities ) {
				entity.AddComponent<DestroyEvent>();
			}
		}
	}
}