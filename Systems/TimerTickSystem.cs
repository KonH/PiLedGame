using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
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