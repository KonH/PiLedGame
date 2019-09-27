using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class SpawnComponent : IComponent {
		public readonly SpawnRequestType Request;

		public SpawnComponent(SpawnRequestType request) {
			Request = request;
		}
	}
}