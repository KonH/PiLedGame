using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class TimerTickSystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, timer) in state.Entities.Get<TimerComponent>() ) {
				if ( timer.Time >= timer.Interval ) {
					entity.AddComponent(new TimerTickEvent());
				}
			}
		}
	}
}