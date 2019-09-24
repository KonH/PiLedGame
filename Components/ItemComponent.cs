namespace PiLedGame.Components {
	public sealed class ItemComponent : IComponent {
		public readonly string Type;
		public readonly int    Count;
		public bool IsCollected;

		public ItemComponent(string type, int count = 1) {
			Type  = type;
			Count = count;
		}
	}
}