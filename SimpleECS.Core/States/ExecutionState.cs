using SimpleECS.Core.Components;

namespace SimpleECS.Core.States {
	public sealed class ExecutionState : IComponent {
		public bool IsFinished { get; private set; }

		public void Finish() {
			IsFinished = true;
		}
	}
}