using SimpleECS.Core.Common;

namespace SimpleECS.Core.State {
	public sealed class Input {
		public KeyCode Current { get; private set; }

		public void Assign(KeyCode key) {
			Current = key;
		}

		public void Reset() {
			Current = default;
		}
	}
}