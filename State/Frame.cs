using System.Drawing;

namespace PiLedGame.State {
	public sealed class Frame {
		Color[,] _value;

		public Frame(Screen screen) {
			_value = new Color[screen.Width, screen.Height];
		}

		public Color this[int x, int y] {
			get { return _value[x, y]; }
			set { _value[x, y] = value; }
		}

		public void Clear() {
			var width = _value.GetLength(0);
			var height = _value.GetLength(1);
			for ( var y = 0; y < height; y++ ) {
				for ( var x = 0; x < width; x++ ) {
					_value[x, y] = default;
				}
			}
		}
	}
}