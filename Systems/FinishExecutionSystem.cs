using PiLedGame.Common;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class FinishExecutionSystem : ISystem {
		public void Update(GameState state) {
			if ( state.Input.Current == KeyCode.Escape ) {
				state.Execution.Finish();
			}
		}
	}
}