using SimpleECS.Core.Common;
using SimpleECS.Core.Systems;

namespace ShootGame.Unity {
	class ReadUnityInputSystem : ReadInputSystemBase {
		public override (bool, KeyCode) TryReadKey() {
			// TODO: Input
			return (UnityEngine.Input.anyKey, default);
		}
	}
}