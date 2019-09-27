using SimpleECS.Core.Common;

namespace SimpleECS.Core.Events {
	public sealed class AddItemEvent : IEvent {
		public readonly ItemType Item;

		public AddItemEvent(ItemType item) {
			Item = item;
		}
	}
}