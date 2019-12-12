using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class CollectItemSystem : EntityComponentSystem<InventoryComponent, CollisionEvent> {
		public override void Update(Entity entity, InventoryComponent _, CollisionEvent collision) {
			var item = collision.Other.GetComponent<ItemComponent>();
			if ( item == null ) {
				return;
			}
			collision.Other.AddComponent<CollectItemEvent>();
			entity.AddComponent<AddItemEvent>().Init(item.Item);
		}
	}
}