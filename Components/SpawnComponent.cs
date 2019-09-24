using System;
using PiLedGame.Common;
using PiLedGame.Entities;
using PiLedGame.State;

namespace PiLedGame.Components {
	public sealed class SpawnComponent : IComponent {
		public readonly Action<Entity, Point2D, Point2D> Factory;
		public readonly Func<GameState, bool>            Condition;

		public bool    ShouldSpawn;
		public Point2D Direction;

		public SpawnComponent(
			Action<Entity, Point2D, Point2D> factory, Func<GameState, bool> condition = null, Point2D direction = default) {
			Factory   = factory;
			Condition = condition ?? (_ => true);
			Direction = direction;
		}
	}
}