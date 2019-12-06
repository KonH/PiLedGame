using SimpleECS.Core.Common;

namespace SimpleECS.Core.Configs {
	public sealed class KeyboardSpawnConfig {
		public readonly KeyCode          Key;
		public readonly SpawnRequestType Request;

		public KeyboardSpawnConfig(KeyCode key, SpawnRequestType request) {
			Key     = key;
			Request = request;
		}
	}
}