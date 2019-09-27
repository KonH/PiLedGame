using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class PerformRandomSpawnSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, spawn, _, _) in state.Entities.Get<SpawnComponent, RandomSpawnComponent, TimerTickEvent>() ) {
				entity.AddComponent(new SpawnEvent(spawn.Request));
			}
		}
	}
}