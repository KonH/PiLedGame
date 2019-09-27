namespace SimpleECS.Core.State {
	public sealed class Execution {
		public bool IsFinished { get; private set; }

		public void Finish() {
			IsFinished = true;
		}
	}
}