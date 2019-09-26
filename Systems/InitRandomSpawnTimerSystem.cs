using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class InitRandomSpawnTimerSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, spawn) in state.Entities.Get<RandomSpawnComponent>() ) {
				if ( entity.GetComponent<TimerComponent>() != null ) {
					continue;
				}
				var interval = Random(state, spawn.MinInterval, spawn.MaxInterval);
				entity.AddComponent(new TimerComponent(interval));
			}
		}

		double Random(GameState state, double min, double max) {
			return min + state.Random.NextDouble() * (max - min);
		}
	}
}