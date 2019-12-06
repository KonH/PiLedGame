using SimpleECS.Core.Common;

namespace ShootGame.Logic.Configs {
	public sealed class PreventHealthSpawnConfig {
		public readonly SpawnRequestType Request;
		public readonly int              MinHealth;

		public PreventHealthSpawnConfig(SpawnRequestType request, int minHealth) {
			Request   = request;
			MinHealth = minHealth;
		}
	}
}