using System;
using SimpleECS.Core.Common;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Configs {
	public sealed class SpawnConfig {
		public readonly SpawnRequestType        Request;
		public readonly Action<Entity, Point2D> SpawnCallback;

		public SpawnConfig(SpawnRequestType request, Action<Entity, Point2D> spawnCallback) {
			Request       = request;
			SpawnCallback = spawnCallback;
		}
	}
}