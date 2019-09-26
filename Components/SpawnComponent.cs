namespace PiLedGame.Components {
	public sealed class SpawnComponent : IComponent {
		public readonly string RequestId;

		public SpawnComponent(string requestId) {
			RequestId = requestId;
		}
	}
}