using System;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class UpdateFadeRenderSystem : ComponentSystem<RenderComponent, FadeRenderComponent> {
		public override void Update(ComponentCollection<RenderComponent, FadeRenderComponent> components) {
			foreach ( var (render, fade) in components ) {
				fade.Accum += fade.Decrease;
				while ( ((int) fade.Accum) > 0 ) {
					var color = render.Color;
					var newAlpha = Math.Max(color.A - 1, 0);
					render.Color = color.ChangeAlpha(newAlpha);
					fade.Accum--;
				}
			}
		}
	}
}