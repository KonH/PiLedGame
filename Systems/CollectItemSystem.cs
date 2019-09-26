using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class CollectItemSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, _, collision) in state.Entities.Get<InventoryComponent, CollisionEvent>() ) {
				var item = collision.Other.GetComponent<ItemComponent>();
				if ( item == null ) {
					continue;
				}
				collision.Other.AddComponent(new CollectItemEvent());
				entity.AddComponent(new AddItemEvent(item));
			}
		}
	}
}