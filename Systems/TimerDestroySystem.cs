using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class TimerDestroySystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (entity, timer, _) in state.Entities.Get<TimerComponent, TimerTickEvent>() ) {
				entity.RemoveComponent(timer);
			}
		}
	}
}