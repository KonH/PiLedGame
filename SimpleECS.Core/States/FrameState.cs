using SimpleECS.Core.Common;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;

namespace SimpleECS.Core.States {
	public sealed class FrameState : IComponent {
		Color[,] _value;

		public Color this[int x, int y] {
			get { return _value[x, y]; }
		}

		public FrameState(ScreenConfig screen) {
			_value = new Color[screen.Width, screen.Height];
		}

		public void ChangeAt(Point2D position, Color color) {
			var x = position.X;
			var y = position.Y;
			if ( (x < 0) || (x >= _value.GetLength(0)) ) {
				return;
			}
			if ( (y < 0) || (y >= _value.GetLength(1)) ) {
				return;
			}
			_value[x, y] = color;
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