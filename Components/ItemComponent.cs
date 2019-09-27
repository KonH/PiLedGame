using PiLedGame.Common;

namespace PiLedGame.Components {
	public sealed class ItemComponent : IComponent {
		public readonly ItemType Item;

		public ItemComponent(ItemType item) {
			Item = item;
		}
	}
}