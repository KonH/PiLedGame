using System;
using System.Globalization;
using PiLedGame.Common;

namespace PiLedGame.State {
	public sealed class InputFrame {
		public readonly double  Time;
		public readonly KeyCode Key;

		public InputFrame(double time, KeyCode key) {
			Time = time;
			Key  = key;
		}

		public override string ToString() {
			return $"{Time.ToString(CultureInfo.InvariantCulture)}={Key.ToString()}";
		}

		public static InputFrame Parse(string str) {
			var parts = str.Split('=');
			var time = double.Parse(parts[0]);
			var key = Enum.Parse<KeyCode>(parts[1]);
			return new InputFrame(time, key);
		}
	}
}