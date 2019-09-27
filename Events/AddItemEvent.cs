using PiLedGame.Common;

namespace PiLedGame.Events {
	public sealed class AddItemEvent : IEvent {
		public readonly ItemType Item;

		public AddItemEvent(ItemType item) {
			Item = item;
		}
	}
}