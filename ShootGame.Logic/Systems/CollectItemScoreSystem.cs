using ShootGame.Logic.Events;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;

namespace ShootGame.Logic.Systems {
	public sealed class CollectItemScoreSystem : EntityComponentSystem<AddItemEvent> {
		readonly int _score;

		public CollectItemScoreSystem(int score) {
			_score = score;
		}

		public override void Update(EntityComponentCollection<AddItemEvent> entities) {
			foreach ( var (e, _) in entities ) {
				e.AddComponent<AddScoreEvent>().Init(_score);
			}
		}
	}
}