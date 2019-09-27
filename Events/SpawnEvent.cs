using PiLedGame.Common;

namespace PiLedGame.Events {
	public sealed class SpawnEvent : IEvent {
		public readonly SpawnRequestType Request;

		public SpawnEvent(SpawnRequestType request) {
			Request = request;
		}
	}
}