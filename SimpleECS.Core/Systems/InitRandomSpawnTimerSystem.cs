using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class InitRandomSpawnTimerSystem : ISystem {
		public void Update(EntitySet entities) {
			foreach ( var (entity, spawn) in entities.Get<RandomSpawnComponent>() ) {
				if ( entity.GetComponent<TimerComponent>() != null ) {
					continue;
				}
				var random = entities.GetFirstComponent<RandomState>();
				var interval = Random(random, spawn.MinInterval, spawn.MaxInterval);
				entity.AddComponent<TimerComponent>().Init(interval);
			}
		}

		double Random(RandomState random, double min, double max) {
			return min + random.Generator.NextDouble() * (max - min);
		}
	}
}