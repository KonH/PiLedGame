using System.Collections.Generic;
using SimpleECS.Core.Common;

namespace SimpleECS.Core.Configs {
	public sealed class PreventSpawnCollisionConfig {
		public readonly HashSet<SpawnRequestType> Requests;

		public PreventSpawnCollisionConfig(params SpawnRequestType[] requests) {
			Requests = new HashSet<SpawnRequestType>();
		}
	}
}