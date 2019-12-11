using System;

namespace SimpleECS.Core.Common {
	public struct SpawnRequestType {
		readonly string _value;

		SpawnRequestType(string value) {
			_value = value;
		}

		public bool Equals(SpawnRequestType other) {
			return string.Equals(_value, other._value, StringComparison.Ordinal);
		}

		public override bool Equals(object obj) {
			return obj is SpawnRequestType other && Equals(other);
		}

		public override int GetHashCode() {
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value);
		}

		public static bool operator ==(SpawnRequestType left, SpawnRequestType right) {
			return left.Equals(right);
		}

		public static bool operator !=(SpawnRequestType left, SpawnRequestType right) {
			return !left.Equals(right);
		}

		public static SpawnRequestType Of(string value) {
			return new SpawnRequestType(value);
		}
	}
}