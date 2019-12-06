using System.Collections.Generic;
using SimpleECS.Core.Common;
using SimpleECS.Core.Systems;

namespace ShootGame.Unity {
	class ReadUnityInputSystem : BaseReadInputSystem {
		readonly Dictionary<UnityEngine.KeyCode, KeyCode> _controls;

		public ReadUnityInputSystem(Dictionary<UnityEngine.KeyCode, KeyCode> controls) {
			_controls = controls;
		}

		public override (bool, KeyCode) TryReadKey() {
			foreach ( var pair in _controls ) {
				if ( UnityEngine.Input.GetKeyDown(pair.Key) ) {
					return (true, pair.Value);
				}
			}
			return (false, default);
		}
	}
}