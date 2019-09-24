using System.Collections.Generic;

namespace PiLedGame.Components {
	public sealed class InventoryComponent : IComponent {
		Dictionary<string, int> _items = new Dictionary<string, int>();

		public void AddItem(string type) {
			_items[type] = _items.GetValueOrDefault(type) + 1;
		}

		public bool TryGetItem(string type) {
			if ( !_items.TryGetValue(type, out var count) || (count <= 0) ) {
				return false;
			}
			var newCount = count - 1;
			if ( newCount == 0 ) {
				_items.Remove(type);
			} else {
				_items[type] = newCount;
			}
			return true;
		}
	}
}