using System;
using System.Drawing;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SpawnSystem : ISystem {
		public void Update(GameState state) {
			var key = state.Input.Current;
			var direction = GetDirection(key);
			if ( (direction.X == 0) && (direction.Y == 0) ) {
				return;
			}
			foreach ( var entity in state.Entities.All ) {
				var source = entity.GetComponent<SpawnSourceComponent>();
				if ( source == null ) {
					continue;
				}
				var position = entity.GetComponent<PositionComponent>();
				if ( position == null ) {
					continue;
				}
				Spawn(state.Entities, position.Point, direction);
			}
			state.Entities.FlushNewEntities();
		}

		Point2D GetDirection(ConsoleKey key) {
			switch ( key ) {
				case ConsoleKey.UpArrow:    return new Point2D(0, -1);
				case ConsoleKey.DownArrow:  return new Point2D(0, 1);
				case ConsoleKey.LeftArrow:  return new Point2D(-1, 0);
				case ConsoleKey.RightArrow: return new Point2D(1, 0);
				default:                    return new Point2D(0, 0);
			}
		}

		void Spawn(EntitySet entities, Point2D origin, Point2D direction) {
			var bullet = entities.AddEntity();
			var position = origin + direction;
			bullet.AddComponent(new PositionComponent(position));
			bullet.AddComponent(new RenderComponent(Color.Red));
			bullet.AddComponent(new TrailComponent(Color.Firebrick));
			bullet.AddComponent(new LinearMovementComponent(direction, 0.33));
			bullet.AddComponent(new OutOfBoundsDestroyComponent());
		}
	}
}