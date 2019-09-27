using SimpleECS.Core.State;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class RenderFrameSystem : ISystem {
		public void Update(GameState state) {
			var graphics = state.Graphics;
			var frame = graphics.Frame;
			foreach ( var (_, position, render) in state.Entities.Get<PositionComponent, RenderComponent>() ) {
				frame.ChangeAt(position.Point, render.Color);
			}
		}
	}
}