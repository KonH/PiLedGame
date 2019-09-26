namespace PiLedGame.Events {
	public sealed class SpawnEvent : IEvent {
		public readonly string RequestId;

		public SpawnEvent(string requestId) {
			RequestId = requestId;
		}
	}
}