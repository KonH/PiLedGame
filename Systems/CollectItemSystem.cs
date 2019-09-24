using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class CollectItemSystem : ISystem {
		public void Update(GameState state) {
			var inventories = state.Entities.Get<InventoryComponent, PositionComponent>();
			foreach ( var (_, item, itemPosition) in state.Entities.Get<ItemComponent, PositionComponent>() ) {
				foreach ( var (_, inventory, invPosition) in inventories ) {
					if ( itemPosition.Point != invPosition.Point ) {
						continue;
					}
					item.IsCollected = true;
					inventory.AddItem(item.Type, item.Count);
					break;
				}
			}
		}
	}
}