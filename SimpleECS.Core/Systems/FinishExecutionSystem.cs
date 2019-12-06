using SimpleECS.Core.Common;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class FinishExecutionSystem : ISystem {
		public void Update(EntitySet entities) {
			var isFinished = CheckFinish(entities);
			if ( isFinished ) {
				MarkFinished(entities);
			}
		}

		bool CheckFinish(EntitySet entities) {
			foreach ( var (_, input) in entities.Get<InputState>() ) {
				if ( input.Current == KeyCode.Escape ) {
					return true;
				}
			}
			return false;
		}

		void MarkFinished(EntitySet entities) {
			foreach ( var exec in entities.GetComponent<ExecutionState>() ) {
				exec.Finish();
			}
		}
	}
}