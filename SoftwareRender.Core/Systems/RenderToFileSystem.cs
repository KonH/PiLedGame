using SimpleECS.Core.State;
using SimpleECS.Core.Systems;
using SkiaSharp;

namespace SoftwareRender.Core.Systems {
	public sealed class RenderToFileSystem : ISystem {
		readonly string _fileNameFormat;

		int _frameIndex = 0;

		public RenderToFileSystem(string fileNameFormat) {
			_fileNameFormat = fileNameFormat;
		}

		public void Update(GameState state) {
			var screen = state.Graphics.Screen;
			using var bitmap = new SKBitmap(screen.Width, screen.Height);
			var frame = state.Graphics.Frame;
			for ( var y = 0; y < screen.Height; y++ ) {
				for ( var x = 0; x < screen.Width; x++ ) {
					var c = frame[x, y];
					var color = new SKColor(c.R, c.G, c.B, c.A);
					bitmap.SetPixel(x, y, color);
				}
			}
			var fileName = string.Format(_fileNameFormat, _frameIndex.ToString());
			using var stream = new SKFileWStream(fileName);
			bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
			_frameIndex++;
		}
	}
}