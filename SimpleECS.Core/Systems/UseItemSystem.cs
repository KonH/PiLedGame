using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class UseItemSystem<T> : ISystem where T : IEvent, new() {
		readonly ItemType _item;

		public UseItemSystem(ItemType item) {
			_item = item;
		}

		public void Update(GameState state) {
			foreach ( var (entity, inv) in state.Entities.Get<InventoryComponent>() ) {
				if ( inv.TryGetItem(_item) ) {
					entity.AddComponent(new T());
				}
			}
		}
	}
}