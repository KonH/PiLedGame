using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class RemoveHiddenFadeRenderSystem : EntityComponentSystem<RenderComponent, FadeRenderComponent> {
		public override void Update(Entity entity, RenderComponent render, FadeRenderComponent _) {
			if ( render.Color.A == 0 ) {
				entity.AddComponent<DestroyEvent>();
			}
		}
	}
}