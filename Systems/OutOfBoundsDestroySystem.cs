using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class OutOfBoundsDestroySystem : ISystem {
		public void Update(GameState state) {
			var screen = state.Graphics.Screen;
			var width = screen.Width;
			var height = screen.Height;
			foreach ( var entity in state.Entities.All ) {
				var destroy = entity.GetComponent<OutOfBoundsDestroyComponent>();
				if ( destroy == null ) {
					continue;
				}
				var position = entity.GetComponent<PositionComponent>();
				if ( position == null ) {
					continue;
				}
				var point = position.Point;
				if ( (point.X < 0) || (point.X >= width) || (point.Y < 0) || (point.Y >= height) ) {
					state.Entities.RemoveEntity(entity);
				}
			}
			state.Entities.FlushRemovedEntities();
		}
	}
}