using PiLedGame.Common;

namespace PiLedGame.Components {
	public sealed class SpawnComponent : IComponent {
		public readonly SpawnRequestType Request;

		public SpawnComponent(SpawnRequestType request) {
			Request = request;
		}
	}
}