using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class PerformRandomSpawnSystem : EntityComponentSystem<SpawnComponent, RandomSpawnComponent, TimerTickEvent> {
		public override void Update(Entity entity, SpawnComponent spawn, RandomSpawnComponent _, TimerTickEvent __) {
			entity.AddComponent<SpawnEvent>().Init(spawn.Request);
		}
	}
}