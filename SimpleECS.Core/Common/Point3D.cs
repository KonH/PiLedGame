namespace SimpleECS.Core.Common {
	public struct Point3D {
		public static Point3D Zero  => new Point3D(0, 0, 0);
		public static Point3D Up    => new Point3D(0, 1, 0);
		public static Point3D Down  => new Point3D(0, -1, 0);
		public static Point3D Left  => new Point3D(-1, 0, 0);
		public static Point3D Right => new Point3D(1, 0, 0);

		public readonly float X;
		public readonly float Y;
		public readonly float Z;

		public Point3D(float x, float y, float z) {
			X = x;
			Y = y;
			Z = z;
		}

		public override string ToString() {
			return $"({X}, {Y}, {Z})";
		}

		public static Point3D operator +(Point3D p1, Point3D p2) {
			return new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
		}
	}
}