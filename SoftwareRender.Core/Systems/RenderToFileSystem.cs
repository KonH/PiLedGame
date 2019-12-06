using SimpleECS.Core.Configs;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;
using SkiaSharp;
using SoftwareRender.Core.Configs;

namespace SoftwareRender.Core.Systems {
	public sealed class RenderToFileSystem : SingleComponentSystem<FrameState> {
		readonly ScreenConfig       _screen;
		readonly RenderToFileConfig _config;

		int _frameIndex = 0;

		public RenderToFileSystem(ScreenConfig screen, RenderToFileConfig config) {
			_screen = screen;
			_config = config;
		}

		public override void Update(FrameState frame) {
			using var bitmap = new SKBitmap(_screen.Width, _screen.Height);
			for ( var y = 0; y < _screen.Height; y++ ) {
				for ( var x = 0; x < _screen.Width; x++ ) {
					var c = frame[x, y];
					var color = new SKColor(c.R, c.G, c.B, c.A);
					bitmap.SetPixel(x, y, color);
				}
			}
			var fileName = string.Format(_config.FileNameFormat, _frameIndex.ToString());
			using var stream = new SKFileWStream(fileName);
			bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
			_frameIndex++;
		}
	}
}