using System;
using rpi_ws281x;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;
using SimpleECS.RaspberryPi.Configs;

namespace SimpleECS.RaspberryPi.Systems {
	public sealed class DeviceRenderSystem : BaseRenderSystem, IInit, IDisposable {
		readonly ScreenConfig       _screen;
		readonly DeviceRenderConfig _config;

		Controller _controller;
		WS281x     _device;

		public DeviceRenderSystem(ScreenConfig screen, DeviceRenderConfig config) {
			_screen = screen;
			_config = config;
		}

		public void Init(EntitySet entities) {
			var settings = Settings.CreateDefaultSettings();
			var ledCount = _screen.Width * _screen.Height;
			try {
				_controller = settings.AddController(
					ledCount, Pin.Gpio18, StripType.WS2812_STRIP, brightness: _config.Brightness);
				_device = new WS281x(settings);
			} catch ( DllNotFoundException e ) {
				var debug = entities.GetFirstComponent<DebugState>();
				debug?.Log($"Failed to load device DLL (it's fine for debug): {e.Message}");
			}
		}

		public override void Update(FrameState frame) {
			if ( (_controller == null) || (_device == null) ) {
				return;
			}
			var width = _screen.Width;
			var height = _screen.Height;
			for ( var y = 0; y < height; y++ ) {
				for ( var x = 0; x < width; x++ ) {
					var id = y * width + x;
					var color = frame[x, y];
					_controller.SetLED(id, System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
				}
			}
			_device.Render();
		}

		public void Dispose() {
			_device?.Reset();
			_device?.Dispose();
		}
	}
}