using System.Collections.Generic;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class PerformRandomSpawnSystem : EntityComponentSystem<SpawnComponent, RandomSpawnComponent, TimerTickEvent> {
		public override void Update(List<(Entity, SpawnComponent, RandomSpawnComponent, TimerTickEvent)> entities) {
			foreach ( var (entity, spawn, _, _) in entities ) {
				entity.AddComponent(new SpawnEvent(spawn.Request));
			}
		}
	}
}