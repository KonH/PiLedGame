using ShootGame.Logic.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;

namespace ShootGame.Logic.Systems {
	public sealed class DestroyUnitScoreSystem : EntityComponentSystem<HealthComponent, DestroyEvent> {
		readonly int _score;

		public DestroyUnitScoreSystem(int score) {
			_score = score;
		}

		public override void Update(EntityComponentCollection<HealthComponent, DestroyEvent> entities) {
			foreach ( var (e, health, _) in entities ) {
				if ( health.Layer.IsEmpty ) {
					e.AddComponent<AddScoreEvent>().Init(_score);
				}
			}
		}
	}
}