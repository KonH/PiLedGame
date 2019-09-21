using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class GameOverSystem : ISystem {
		public void Update(GameState state) {
			if ( state.Entities.Get<PlayerComponent>().Count == 0 ) {
				state.Execution.Finish();
			}
		}
	}
}