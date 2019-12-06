using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class RenderFrameSystem : ISystem {
		public void Update(EntitySet entities) {
			foreach ( var frame in entities.GetComponent<FrameState>() ) {
				foreach ( var (position, render) in entities.GetComponent<PositionComponent, RenderComponent>() ) {
					frame.ChangeAt(position.Point, render.Color);
				}
			}
		}
	}
}