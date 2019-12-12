using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class TrailComponent : IComponent {
		public double WantedTime;
		public Color  Color;

		public void Init(double wantedTime, Color color) {
			WantedTime = wantedTime;
			Color      = color;
		}

		bool Equals(TrailComponent other) {
			return WantedTime.Equals(other.WantedTime) && Color.Equals(other.Color);
		}

		public override bool Equals(object obj) {
			return ReferenceEquals(this, obj) || (obj is TrailComponent other) && Equals(other);
		}

		public override int GetHashCode() {
			unchecked {
				return (WantedTime.GetHashCode() * 397) ^ Color.GetHashCode();
			}
		}
	}
}