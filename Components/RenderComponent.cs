using System.Drawing;

namespace PiLedGame.Components {
	public sealed class RenderComponent : IComponent {
		public Color Color;

		public RenderComponent(Color color) {
			Color = color;
		}
	}
}