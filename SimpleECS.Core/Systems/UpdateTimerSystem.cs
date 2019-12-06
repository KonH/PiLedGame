using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class UpdateTimerSystem : ISystem {
		public void Update(EntitySet entities) {
			var time = entities.GetFirstComponent<TimeState>();
			var dt = time.DeltaTime;
			foreach ( var (_, timer) in entities.Get<TimerComponent>() ) {
				timer.Time += dt;
			}
		}
	}
}