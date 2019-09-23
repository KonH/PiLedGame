using System;
using PiLedGame.Common;

namespace PiLedGame.Components {
	public sealed class KeyboardMovementComponent : IComponent {
		public readonly Func<ConsoleKey, Point2D> Movement;

		public KeyboardMovementComponent(Func<ConsoleKey, Point2D> movement) {
			Movement = movement;
		}
	}
}