using System;
using PiLedGame.Common;
using PiLedGame.Entities;

namespace PiLedGame.Components {
	public sealed class SpawnComponent : IComponent {
		public readonly Action<Entity, Point2D, Point2D> Factory;

		public bool    ShouldSpawn;
		public Point2D Direction;

		public SpawnComponent(Action<Entity, Point2D, Point2D> factory, Point2D direction = default) {
			Factory   = factory;
			Direction = direction;
		}
	}
}