using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class CollectItemSystem : EntityComponentSystem<InventoryComponent, CollisionEvent> {
		public override void Update(EntityComponentCollection<InventoryComponent, CollisionEvent> entities) {
			foreach ( var (entity, _, collision) in entities ) {
				var item = collision.Other.GetComponent<ItemComponent>();
				if ( item == null ) {
					continue;
				}
				collision.Other.AddComponent<CollectItemEvent>();
				entity.AddComponent<AddItemEvent>().Init(item.Item);
			}
		}
	}
}