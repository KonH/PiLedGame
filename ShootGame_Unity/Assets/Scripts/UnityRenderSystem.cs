using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;

namespace ShootGame.Unity {
	class UnityRenderSystem : BaseRenderSystem, IInit {
		readonly ScreenConfig  _screen;
		readonly UnityRenderer _renderer;

		public UnityRenderSystem(ScreenConfig screen, UnityRenderer renderer) {
			_screen   = screen;
			_renderer = renderer;
		}

		public void Init(EntitySet entities) {
			_renderer.Init(_screen.Width, _screen.Height);
		}

		public override void Update(FrameState frame) {
			for ( var x = 0; x < _screen.Width; x++ ) {
				for ( var y = 0; y < _screen.Height; y++ ) {
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