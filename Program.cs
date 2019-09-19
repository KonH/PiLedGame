using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using rpi_ws281x;

namespace PiLedGame {
	class Program {
		class RenderSystem : IDisposable {
			readonly int _width;
			readonly int _height;
			readonly Controller _controller;
			readonly WS281x _device;
			
			object _lock = new object();
			volatile bool _disposed = false;
			int _frame;

			public RenderSystem(int width, int height) {
				var settings = Settings.CreateDefaultSettings();
				var ledCount = width * height;
				_width = width;
				_height = height;
				_controller = settings.AddController(ledCount, Pin.Gpio18, StripType.WS2812_STRIP);
				_device = new WS281x(settings);
			}

			public bool Render(Color[,] screen) {
				if ( screen.GetLength(0) != _width ) {
					throw new InvalidOperationException("Invalid width!");
				}
				if ( screen.GetLength(1) != _height ) {
					throw new InvalidOperationException("Invalid height!");
				}
				lock ( _lock ) {
					if ( _disposed ) {
						return false;
					}
					for ( var y = 0; y < _height; y++ ) {
						for ( var x = 0; x < _width; x++ ) {
							var id = y * _width + x;
							_controller.SetLED(id, screen[x, y]);
						}
					}
					_device.Render();
				}
				_frame++;
				return true;
			}

			public void Dispose() {
				lock ( _lock ) {
					_device.Reset();
					_device.Dispose();
					_disposed = true;
				}
			}
		}

		struct Point {
			public int X;
			public int Y;

			public Point(int x, int y) {
				X = x;
				Y = y;
			}

			public static Point operator +(Point p1, Point p2) {
				return new Point(p1.X + p2.X, p1.Y + p2.Y);
			}
		}

		class FrameBuffer {
			public Color[,] Current;
			public Color[,] Next;

			readonly int _width;
			readonly int _height;

			public FrameBuffer(int width, int height) {
				_width = width;
				_height = height;
				Current = new Color[_width, _height];
				Next = new Color[_width, _height];
			}

			public void Clear() {
				for ( var y = 0; y < _height; y++ ) {
					for ( var x = 0; x < _width; x++ ) {
						Next[x, y] = default;
					}
				}
			}

			public void Swap() {
				var tmp = Current;
				Current = Next;
				Next = tmp;
			}
		}

		static void Main(string[] args) {
			var width = 8;
			var height = 8;
			var frameBuffer = new FrameBuffer(width, height);
			var renderSystem = new RenderSystem(width, height);
			var renderThread = new Thread(() => RenderThread(renderSystem, frameBuffer));
			renderThread.Start();
			using ( renderSystem ) {
				MainThread(width, height, frameBuffer);
			}
		}

		static void RenderThread(RenderSystem renderSystem, FrameBuffer frameBuffer) {
			while ( true ) {
				var screen = frameBuffer.Current;
				if ( !renderSystem.Render(screen) ) {
					break;
				}
			}
		}

		static void MainThread(int width, int height, FrameBuffer frameBuffer) {
			var point = new Point();
			while ( true ) {
				Console.Clear();
				frameBuffer.Clear();
				var buffer = frameBuffer.Next;
				var playerColor = Color.Green;
				buffer[point.X, point.Y] = playerColor;
				frameBuffer.Swap();
				ConsoleRender(frameBuffer);
				var key = Console.ReadKey().Key;
				if ( key == ConsoleKey.Escape ) {
					return;
				}
				point += Direction(key);
				point = FitToScreen(point, width, height);
			}
		}

		static void ConsoleRender(FrameBuffer frameBuffer) {
			Console.WriteLine();
			var buffer = frameBuffer.Current;
			for ( var y = 0; y < 8; y++ ) {
				for ( var x = 0; x < 8; x++ ) {
					Console.BackgroundColor = FromColor(buffer[x, y]);
					Console.Write(" x ");
					Console.BackgroundColor = ConsoleColor.Black;
				}
				Console.WriteLine();
			}
		}

		public static ConsoleColor FromColor(Color c) {
			int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
			index |= (c.R > 64) ? 4 : 0; // Red bit
			index |= (c.G > 64) ? 2 : 0; // Green bit
			index |= (c.B > 64) ? 1 : 0; // Blue bit
			return (ConsoleColor)index;
		}

		static Point Direction(ConsoleKey key) {
			switch ( key ) {
				case ConsoleKey.W: return new Point(0, -1);
				case ConsoleKey.S: return new Point(0, 1);
				case ConsoleKey.A: return new Point(-1, 0);
				case ConsoleKey.D: return new Point(1, 0);
			}
			return default;
		}

		static Point FitToScreen(Point point, int width, int height) {
			return new Point(Fit(point.X, 0, width), Fit(point.Y, 0, height));
		}

		static int Fit(int value, int min, int max) {
			if ( value >= max ) {
				return min;
			}
			if ( value < min ) {
				return max - 1;
			}
			return value;
		}
	}
}