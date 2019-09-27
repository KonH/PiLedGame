using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class ItemComponent : IComponent {
		public readonly ItemType Item;

		public ItemComponent(ItemType item) {
			Item = item;
		}
	}
}