using System;

namespace PiLedGame.Components {
	public sealed class KeyboardSpawnComponent : IComponent {
		public readonly ConsoleKey Trigger;

		public KeyboardSpawnComponent(ConsoleKey trigger) {
			Trigger = trigger;
		}
	}
}