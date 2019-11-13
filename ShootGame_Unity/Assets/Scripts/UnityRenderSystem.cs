using SimpleECS.Core.State;
using SimpleECS.Core.Systems;

namespace ShootGame.Unity {
	class UnityRenderSystem : IRenderSystem, IInit {
		UnityRenderer _renderer;

		public UnityRenderSystem(UnityRenderer renderer) {
			_renderer = renderer;
		}

		public void Init(GameState state) {
			var screen = state.Graphics.Screen;
			_renderer.Init(screen.Width, screen.Height);
		}

		public void Update(GameState state) {
			var screen = state.Graphics.Screen;
			var frame = state.Graphics.Frame;
			for ( var x = 0; x < screen.Width; x++ ) {
				for ( var y = 0; y < screen.Height; y++ ) {
					var color = frame[x, y];
					_renderer.SetPixel(
						x, y,
						Normalize(color.R), Normalize(color.G), Normalize(color.B), Normalize(color.A));
				}
			}
			_renderer.Apply();
		}

		static float Normalize(byte value) {
			return ((float) value) / 255;
		}
	}
}