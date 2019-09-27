using SimpleECS.Core.State;
using SimpleECS.Core.Common;

namespace SimpleECS.Core.Systems {
	public sealed class FinishExecutionSystem : ISystem {
		public void Update(GameState state) {
			if ( state.Input.Current == KeyCode.Escape ) {
				state.Execution.Finish();
			}
		}
	}
}