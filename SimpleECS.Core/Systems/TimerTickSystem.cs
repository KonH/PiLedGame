using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class TimerTickSystem : EntityComponentSystem<TimerComponent> {
		public override void Update(EntityComponentCollection<TimerComponent> entities) {
			foreach ( var (entity, timer) in entities ) {
				if ( timer.Time >= timer.Interval ) {
					entity.AddComponent<TimerTickEvent>();
				}
			}
		}
	}
}