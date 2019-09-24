using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class ScoreMeasureSystem : ISystem {
		public int TotalScore = 0;

		public void Update(GameState state) {
			TotalScore += GetDestroyedUnits(state.Entities) * 10;
			TotalScore += GetCollectedItems(state.Entities) * 50;
		}

		int GetDestroyedUnits(EntitySet entities) {
			var accum = 0;
			foreach ( var (_, health) in entities.Get<HealthComponent>() ) {
				if ( (health.Health == 0) && (health.Layer == null) ) {
					accum++;
				}
			}
			return accum;
		}

		int GetCollectedItems(EntitySet entities) {
			var accum = 0;
			foreach ( var (_, item) in entities.Get<ItemComponent>() ) {
				if ( item.IsCollected ) {
					accum++;
				}
			}
			return accum;
		}
	}
}