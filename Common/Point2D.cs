namespace PiLedGame.Common {
	public struct Point2D {
		public readonly int X;
		public readonly int Y;

		public Point2D(int x, int y) {
			X = x;
			Y = y;
		}

		public static Point2D operator +(Point2D p1, Point2D p2) {
			return new Point2D(p1.X + p2.X, p1.Y + p2.Y);
		}

		public static bool operator ==(Point2D p1, Point2D p2) {
			return (p1.X == p2.X) && (p1.Y == p2.Y);
		}

		public static bool operator !=(Point2D p1, Point2D p2) {
			return !(p1 == p2);
		}

		public bool Equals(Point2D other) {
			return X == other.X && Y == other.Y;
		}

		public override bool Equals(object obj) {
			return obj is Point2D other && Equals(other);
		}

		public override int GetHashCode() {
			unchecked {
				return (X * 397) ^ Y;
			}
		}
	}
}