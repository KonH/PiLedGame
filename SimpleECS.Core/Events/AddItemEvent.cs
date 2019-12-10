using SimpleECS.Core.Common;

namespace SimpleECS.Core.Events {
	public sealed class AddItemEvent : IEvent {
		public ItemType Item { get; private set; }

		public void Init(ItemType item) {
			Item = item;
		}
	}
}