using System;
using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class InitRandomSpawnTimerSystem : ISystem {
		readonly Random _random = new Random();

		public void Update(GameState state) {
			foreach ( var (entity, spawn) in state.Entities.Get<RandomSpawnComponent>() ) {
				if ( entity.GetComponent<TimerComponent>() != null ) {
					continue;
				}
				var interval = Random(spawn.MinInterval, spawn.MaxInterval);
				entity.AddComponent(new TimerComponent(interval));
			}
		}

		double Random(double min, double max) {
			return min + _random.NextDouble() * (max - min);
		}
	}
}