using System;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class RandomSpawnSystem : ISystem {
		readonly Random _random = new Random();

		public void Update(GameState state) {
			var dt = state.Time.DeltaTime;
			foreach ( var (entity, spawn, randomSpawn) in state.Entities.Get<SpawnComponent, RandomSpawnComponent>() ) {
				if ( randomSpawn.Interval <= 0 ) {
					UpdateSpawner(randomSpawn);
				}
				randomSpawn.Timer += dt;
				if ( IsPointBusy(entity, state.Entities) ) {
					continue;
				}
				if ( randomSpawn.Timer >= randomSpawn.Interval ) {
					UpdateSpawner(randomSpawn);
					spawn.ShouldSpawn = true;
				}
			}
		}

		void UpdateSpawner(RandomSpawnComponent randomSpawn) {
			randomSpawn.Interval = Random(randomSpawn.MinInterval, randomSpawn.MaxInterval);
			randomSpawn.Timer    = 0.0;
		}

		double Random(double min, double max) {
			return min + _random.NextDouble() * (max - min);
		}

		bool IsPointBusy(Entity entity, EntitySet entities) {
			var spawnPosition = entity.GetComponent<PositionComponent>();
			foreach ( var (e, position) in entities.Get<PositionComponent>() ) {
				if ( e == entity ) {
					continue;
				}
				if ( spawnPosition.Point == position.Point ) {
					return true;
				}
			}
			return false;
		}
	}
}