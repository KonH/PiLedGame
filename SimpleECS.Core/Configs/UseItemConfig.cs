using SimpleECS.Core.Common;

namespace SimpleECS.Core.Configs {
	public sealed class UseItemConfig {
		public readonly ItemType Item;

		public UseItemConfig(ItemType item) {
			Item = item;
		}
	}
}