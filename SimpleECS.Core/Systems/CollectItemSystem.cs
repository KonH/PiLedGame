using System.Collections.Generic;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class CollectItemSystem : EntityComponentSystem<InventoryComponent, CollisionEvent> {
		public override void Update(List<(Entity, InventoryComponent, CollisionEvent)> entities) {
			foreach ( var (entity, _, collision) in entities ) {
				var item = collision.Other.GetComponent<ItemComponent>();
				if ( item == null ) {
					continue;
				}
				collision.Other.AddComponent(new CollectItemEvent());
				entity.AddComponent(new AddItemEvent(item.Item));
			}
		}
	}
}