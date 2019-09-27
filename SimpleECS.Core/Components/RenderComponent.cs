using System.Drawing;

namespace SimpleECS.Core.Components {
	public sealed class RenderComponent : IComponent {
		public Color Color;

		public RenderComponent(Color color) {
			Color = color;
		}
	}
}