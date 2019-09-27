using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class TimerDestroySystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, timer, _) in state.Entities.Get<TimerComponent, TimerTickEvent>() ) {
				entity.RemoveComponent(timer);
			}
		}
	}
}