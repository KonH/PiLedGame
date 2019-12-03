namespace SoftwareRender.Core.Utils {
	public static class Common {
		public static void Swap<T>(ref T a, ref T b) {
			var tmp = a;
			a = b;
			b = tmp;
		}
	}
}