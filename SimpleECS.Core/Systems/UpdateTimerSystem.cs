using SimpleECS.Core.State;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class UpdateTimerSystem : ISystem {
		public void Update(GameState state) {
			var dt = state.Time.DeltaTime;
			foreach ( var (_, timer) in state.Entities.Get<TimerComponent>() ) {
				timer.Time += dt;
			}
		}
	}
}