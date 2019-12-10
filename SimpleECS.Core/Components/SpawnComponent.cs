using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class SpawnComponent : IComponent {
		public SpawnRequestType Request { get; private set; }

		public SpawnComponent Init(SpawnRequestType request) {
			Request = request;
			return this;
		}
	}
}