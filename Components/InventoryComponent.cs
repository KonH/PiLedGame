using System.Collections.Generic;
using PiLedGame.Common;

namespace PiLedGame.Components {
	public sealed class InventoryComponent : IComponent {
		Dictionary<ItemType, int> _items = new Dictionary<ItemType, int>();

		public void AddItem(ItemType item) {
			_items[item] = _items.GetValueOrDefault(item) + 1;
		}

		public bool TryGetItem(ItemType item) {
			if ( !_items.TryGetValue(item, out var count) || (count <= 0) ) {
				return false;
			}
			var newCount = count - 1;
			if ( newCount == 0 ) {
				_items.Remove(item);
			} else {
				_items[item] = newCount;
			}
			return true;
		}
	}
}