using System;
using System.Globalization;

namespace SimpleECS.Core.Common {
	public sealed class FrameRecord {
		public readonly double  Time;
		public readonly KeyCode Key;

		public FrameRecord(double time, KeyCode key) {
			Time = time;
			Key  = key;
		}

		public override string ToString() {
			return $"{Time.ToString(CultureInfo.InvariantCulture)}={Key.ToString()}";
		}

		public static FrameRecord Parse(string str) {
			var parts = str.Split('=');
			var time = double.Parse(parts[0]);
			var key = (KeyCode)Enum.Parse(typeof(KeyCode), parts[1]);
			return new FrameRecord(time, key);
		}
	}
}