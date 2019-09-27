using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class PerformRandomSpawnSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, spawn, _, _) in state.Entities.Get<SpawnComponent, RandomSpawnComponent, TimerTickEvent>() ) {
				entity.AddComponent(new SpawnEvent(spawn.Request));
			}
		}
	}
}