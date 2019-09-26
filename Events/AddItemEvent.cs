using PiLedGame.Components;

namespace PiLedGame.Events {
	public sealed class AddItemEvent : IEvent {
		public readonly string Type;
		public readonly int    Count;

		public AddItemEvent(ItemComponent item) {
			Type  = item.Type;
			Count = item.Count;
		}
	}
}