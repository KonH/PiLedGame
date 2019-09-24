namespace PiLedGame.Components {
	public sealed class ItemComponent : IComponent {
		public readonly string Type;
		public bool IsCollected;

		public ItemComponent(string type) {
			Type = type;
		}
	}
}