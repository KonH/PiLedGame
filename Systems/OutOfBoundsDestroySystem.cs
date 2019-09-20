using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class OutOfBoundsDestroySystem : ISystem {
		public void Update(GameState state) {
			var screen = state.Graphics.Screen;
			var width = screen.Width;
			var height = screen.Height;
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (entity, _, position) in state.Entities.Get<OutOfBoundsDestroyComponent, PositionComponent>() ) {
					var point = position.Point;
					if ( (point.X < 0) || (point.X >= width) || (point.Y < 0) || (point.Y >= height) ) {
						editor.RemoveEntity(entity);
					}
				}
			}
		}
	}
}