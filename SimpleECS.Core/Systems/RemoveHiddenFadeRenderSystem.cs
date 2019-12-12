using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class RemoveHiddenFadeRenderSystem : EntityComponentSystem<RenderComponent, FadeRenderComponent> {
		public override void Update(EntityComponentCollection<RenderComponent, FadeRenderComponent> components) {
			foreach ( var (e, render, _) in components ) {
				if ( render.Color.A == 0 ) {
					e.AddComponent<DestroyEvent>();
				}
			}
		}
	}
}