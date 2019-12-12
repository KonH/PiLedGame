namespace SimpleECS.Core.Common {
	public struct DamageLayer {
		public bool IsEmpty => _value <= 0;

		readonly int _value;

		DamageLayer(int value) {
			_value = value;
		}

		public bool Equals(DamageLayer other) {
			return _value == other._value;
		}

		public override bool Equals(object obj) {
			return (obj is DamageLayer other) && Equals(other);
		}

		public override int GetHashCode() {
			return _value;
		}

		public static bool operator ==(DamageLayer left, DamageLayer right) {
			return left.Equals(right);
		}

		public static bool operator !=(DamageLayer left, DamageLayer right) {
			return !left.Equals(right);
		}

		public static DamageLayer Of(int value) {
			return new DamageLayer(value);
		}
	}
}