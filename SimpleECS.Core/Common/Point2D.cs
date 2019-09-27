namespace SimpleECS.Core.Common {
	public struct Point2D {
		public static Point2D Zero  => new Point2D(0, 0);
		public static Point2D Up    => new Point2D(0, -1);
		public static Point2D Down  => new Point2D(0, 1);
		public static Point2D Left  => new Point2D(-1, 0);
		public static Point2D Right => new Point2D(1, 0);

		public bool IsEmpty => (X == 0) && (Y == 0);

		public readonly int X;
		public readonly int Y;

		public Point2D(int x, int y) {
			X = x;
			Y = y;
		}

		public override string ToString() {
			return $"({X}, {Y})";
		}

		public bool Equals(Point2D other) {
			return (X == other.X) && (Y == other.Y);
		}

		public override bool Equals(object obj) {
			return (obj is Point2D other) && Equals(other);
		}

		public override int GetHashCode() {
			unchecked {
				return (X * 397) ^ Y;
			}
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
	}
}