using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class TimerDestroySystem : EntityComponentSystem<TimerComponent, TimerTickEvent> {
		public override void Update(EntityComponentCollection<TimerComponent, TimerTickEvent> entities) {
			foreach ( var (entity, timer, _) in entities ) {
				entity.RemoveComponent(timer);
			}
		}
	}
}