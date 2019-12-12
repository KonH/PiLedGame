namespace SimpleECS.Core.Common {
	public struct SpawnRequestType {
		readonly int _value;

		SpawnRequestType(int value) {
			_value = value;
		}

		public bool Equals(SpawnRequestType other) {
			return _value == other._value;
		}

		public override bool Equals(object obj) {
			return obj is SpawnRequestType other && Equals(other);
		}

		public override int GetHashCode() {
			return _value;
		}

		public static bool operator ==(SpawnRequestType left, SpawnRequestType right) {
			return left.Equals(right);
		}

		public static bool operator !=(SpawnRequestType left, SpawnRequestType right) {
			return !left.Equals(right);
		}

		public static SpawnRequestType Of(int value) {
			return new SpawnRequestType(value);
		}
	}
}