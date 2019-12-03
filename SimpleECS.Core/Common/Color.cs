using System;

namespace SimpleECS.Core.Common {
	public struct Color {
		public static Color Red => FromArgb(255, 255, 0, 0);
		public static Color Green => FromArgb(255, 0, 255, 0);
		public static Color Yellow => FromArgb(255, 255, 255, 0);
		public static Color Indigo => FromArgb(255, 75, 0, 130);
		public static Color Firebrick => FromArgb(255, 178, 34, 34);

		public readonly byte A;
		public readonly byte R;
		public readonly byte G;
		public readonly byte B;

		Color(byte a, byte r, byte g, byte b) {
			A = a;
			R = r;
			G = g;
			B = b;
		}

		public Color Multiply(double power) {
			return FromArgb((byte)(R * power), (byte)(G * power), (byte)(B * power));
		}

		public Color ChangeAlpha(int alpha) => FromArgb(alpha, R, G, B);

		public static Color FromArgb(int alpha, int red, int green, int blue) {
			CheckByte(alpha, nameof(alpha));
			CheckByte(red, nameof(red));
			CheckByte(green, nameof(green));
			CheckByte(blue, nameof(blue));

			return new Color((byte)alpha, (byte)red, (byte)green, (byte)blue);
		}

		static void CheckByte(int value, string name) {
			if ( unchecked((uint) value) > byte.MaxValue ) {
				throw new ArgumentException(name);
			}
		}

		public static Color FromArgb(int red, int green, int blue) => FromArgb(byte.MaxValue, red, green, blue);
	}
}