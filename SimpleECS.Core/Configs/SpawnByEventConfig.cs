using SimpleECS.Core.Common;

namespace SimpleECS.Core.Configs {
	public sealed class SpawnByEventConfig {
		public readonly SpawnRequestType Request;

		public SpawnByEventConfig(SpawnRequestType request) {
			Request = request;
		}
	}
}