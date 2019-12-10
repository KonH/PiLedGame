using SimpleECS.Core.Common;

namespace SimpleECS.Core.Events {
	public sealed class SpawnEvent : BaseEvent {
		public SpawnRequestType Request { get; private set; }

		public void Init(SpawnRequestType request) {
			Request = request;
		}
	}
}