using System.Drawing;

namespace PiLedGame.Components {
	public sealed class TrailComponent : IComponent {
		public Color Color;

		public TrailComponent(Color color) {
			Color = color;
		}
	}
}