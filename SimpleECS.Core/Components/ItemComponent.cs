using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class ItemComponent : IComponent {
		public ItemType Item { get; private set; }

		public ItemComponent Init(ItemType item) {
			Item = item;
			return this;
		}
	}
}