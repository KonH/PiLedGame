using System;
using PiLedGame.State;
using rpi_ws281x;

namespace PiLedGame.System {
	public sealed class DeviceRenderSystem : ISystem, IDisposable {
		readonly Controller _controller;
		readonly WS281x     _device;

		public DeviceRenderSystem(GameState state) {
			var screen = state.Graphics.Screen;
			var settings = Settings.CreateDefaultSettings();
			var ledCount = screen.Width * screen.Height;
			_controller = settings.AddController(ledCount, Pin.Gpio18, StripType.WS2812_STRIP);
			_device = new WS281x(settings);
		}

		public void Update(GameState state) {
			var renderFrame = state.Graphics.RenderFrame;
			var screen = state.Graphics.Screen;
			var width = screen.Width;
			var height = screen.Height;
			for ( var y = 0; y < height; y++ ) {
				for ( var x = 0; x < width; x++ ) {
					var id = y * width + x;
					_controller.SetLED(id, renderFrame[x, y]);
				}
			}
			_device.Render();
		}

		public void Dispose() {
			_device.Reset();
			_device.Dispose();
		}
	}
}