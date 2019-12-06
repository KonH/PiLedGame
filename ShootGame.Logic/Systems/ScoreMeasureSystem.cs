using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;

namespace ShootGame.Logic.Systems {
	public sealed class ScoreMeasureSystem : ISystem {
		public int TotalScore = 0;

		public void Update(EntitySet entities) {
			TotalScore += GetDestroyedUnits(entities) * 10;
			TotalScore += GetCollectedItems(entities) * 50;
		}

		int GetDestroyedUnits(EntitySet entities) {
			var accum = 0;
			foreach ( var (_, health, _) in entities.Get<HealthComponent, DestroyEvent>() ) {
				if ( health.Layer.IsEmpty ) {
					accum++;
				}
			}
			return accum;
		}

		int GetCollectedItems(EntitySet entities) {
			var accum = 0;
			foreach ( var (_, _) in entities.Get<AddItemEvent>() ) {
				accum++;
			}
			return accum;
		}
	}
}