using System;
using SimpleECS.Core.Common;

namespace SimpleECS.Core.Configs {
	public sealed class KeyboardMovementConfig {
		public readonly Func<KeyCode, Point2D> KeyCodeToOffset;

		public KeyboardMovementConfig(Func<KeyCode, Point2D> keyCodeToOffset) {
			KeyCodeToOffset = keyCodeToOffset;
		}
	}
}