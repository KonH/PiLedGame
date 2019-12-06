using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class GameOverSystem : ISystem {
		public void Update(EntitySet entities) {
			if ( entities.Get<PlayerComponent>().Count == 0 ) {
				foreach ( var exec in entities.GetComponent<ExecutionState>() ) {
					exec.Finish();
				}
			}
		}
	}
}