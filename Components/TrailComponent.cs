using System.Drawing;

namespace PiLedGame.Components {
	public sealed class TrailComponent : IComponent {
		public double WantedTime;
		public Color  Color;

		public TrailComponent(double wantedTime, Color color) {
			WantedTime = wantedTime;
			Color      = color;
		}
	}
}