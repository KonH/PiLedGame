using SimpleECS.Core.Common;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.States {
	public sealed class InputState : IComponent {
		public KeyCode Current { get; private set; }

		public void Assign(KeyCode key) {
			Current = key;
		}

		public void Reset() {
			Current = default;
		}
	}
}