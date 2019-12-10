using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class TrailComponent : IComponent {
		public double WantedTime;
		public Color  Color;

		public TrailComponent Init(double wantedTime, Color color) {
			WantedTime = wantedTime;
			Color      = color;
			return this;
		}
	}
}