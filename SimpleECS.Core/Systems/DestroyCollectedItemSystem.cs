using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class DestroyCollectedItemSystem : EntityComponentSystem<CollectItemEvent> {
		public override void Update(Entity entity, CollectItemEvent component) {
			entity.AddComponent<DestroyEvent>();
		}
	}
}