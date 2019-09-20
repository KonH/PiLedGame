using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class RenderFrameSystem : ISystem {
		public void Update(GameState state) {
			var graphics = state.Graphics;
			var frame = graphics.Frame;
			foreach ( var entity in state.Entities.All ) {
				var position = entity.GetComponent<PositionComponent>();
				if ( position == null ) {
					continue;
				}
				var render = entity.GetComponent<RenderComponent>();
				if ( render == null ) {
					continue;
				}
				frame.ChangeAt(position.Point, render.Color);
			}
		}
	}
}