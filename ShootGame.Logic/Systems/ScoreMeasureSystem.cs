using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;

namespace ShootGame.Logic.Systems {
	public sealed class ScoreMeasureSystem : ISystem {
		public int TotalScore = 0;

		public void Update(GameState state) {
			TotalScore += GetDestroyedUnits(state.Entities) * 10;
			TotalScore += GetCollectedItems(state.Entities) * 50;
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