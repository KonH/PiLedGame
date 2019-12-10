using SimpleECS.Core.Common;

namespace SimpleECS.Core.Events {
	public sealed class AddItemEvent : BaseEvent {
		public ItemType Item { get; private set; }

		public void Init(ItemType item) {
			Item = item;
		}
	}
}