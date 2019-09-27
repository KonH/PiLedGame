using SimpleECS.Core.Common;

namespace SimpleECS.Core.Events {
	public sealed class SpawnEvent : IEvent {
		public readonly SpawnRequestType Request;

		public SpawnEvent(SpawnRequestType request) {
			Request = request;
		}
	}
}