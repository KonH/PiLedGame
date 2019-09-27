using System;

namespace PiLedGame.Common {
	public struct ItemType {
		readonly string _value;

		ItemType(string value) {
			_value = value;
		}

		public bool Equals(ItemType other) {
			return string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);
		}

		public override bool Equals(object obj) {
			return obj is ItemType other && Equals(other);
		}

		public override int GetHashCode() {
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value);
		}

		public static bool operator ==(ItemType left, ItemType right) {
			return left.Equals(right);
		}

		public static bool operator !=(ItemType left, ItemType right) {
			return !left.Equals(right);
		}

		public static ItemType Of(string value) {
			return new ItemType(value);
		}
	}
}