using SimpleECS.Core.State;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class GameOverSystem : ISystem {
		public void Update(GameState state) {
			if ( state.Entities.Get<PlayerComponent>().Count == 0 ) {
				state.Execution.Finish();
			}
		}
	}
}