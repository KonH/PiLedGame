using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class TimerDestroySystem : EntityComponentSystem<TimerComponent, TimerTickEvent> {
		public override void Update(Entity entity, TimerComponent timer, TimerTickEvent _) {
			entity.RemoveComponent(timer);
		}
	}
}