using System;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class UpdateFadeRenderSystem : ComponentSystem<RenderComponent, FadeRenderComponent> {
		public override void Update(RenderComponent render, FadeRenderComponent fade) {
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