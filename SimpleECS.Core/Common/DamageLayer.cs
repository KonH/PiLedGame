using System;

namespace SimpleECS.Core.Common {
	public struct DamageLayer {
		public bool IsEmpty => string.IsNullOrEmpty(_value);

		readonly string _value;

		DamageLayer(string value) {
			_value = value;
		}

		public bool Equals(DamageLayer other) {
			return string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);
		}

		public override bool Equals(object obj) {
			return obj is DamageLayer other && Equals(other);
		}

		public override int GetHashCode() {
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value);
		}

		public static bool operator ==(DamageLayer left, DamageLayer right) {
			return left.Equals(right);
		}

		public static bool operator !=(DamageLayer left, DamageLayer right) {
			return !left.Equals(right);
		}

		public static DamageLayer Of(string value) {
			return new DamageLayer(value);
		}
	}
}