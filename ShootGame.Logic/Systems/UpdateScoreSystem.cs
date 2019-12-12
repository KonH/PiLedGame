using ShootGame.Logic.Events;
using ShootGame.Logic.States;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Entities;

namespace ShootGame.Logic.Systems {
	public sealed class UpdateScoreSystem : ISystem {
		public void Update(EntitySet entities) {
			var state = entities.GetFirstComponent<ScoreState>();
			foreach ( var e in entities.GetComponent<AddScoreEvent>() ) {
				state.TotalScore += e.Score;
			}
		}
	}
}