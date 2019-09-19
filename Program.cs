using System;
using System.Drawing;
using System.Threading;
using PiLedGame.State;
using PiLedGame.System;
using PiLedGame.Utils;
using PiLedGame.Common;

namespace PiLedGame {
	class Program {
		static void Main(string[] args) {
			var graphics = new Graphics(new Screen(8, 8));
			var state = new GameState(graphics);
			var renderSystems = new SystemSet(new ISystem[] {
				new DeviceRenderSystem(state),
			});
			var abortSignal = new AbortSignal();
			var renderThread = new Thread(() => RenderThread(state, renderSystems, abortSignal));
			renderThread.Start();
			MainThread(state, abortSignal);
		}

		static void RenderThread(GameState state, SystemSet systems, IAbortWatcher abortWatcher) {
			var isFinished = false;
			while ( true ) {
				try {
					if ( !abortWatcher.IsAbortRequested ) {
						systems.Update(state);
					} else {
						systems.TryDispose();
						isFinished = true;
					}
				} catch ( Exception e ) {
					Console.WriteLine(e);
				}
				if ( isFinished ) {
					break;
				}
			}
		}

		static void MainThread(GameState state, AbortSignal abortSignal) {
			var point = new Point2D();
			while ( true ) {
				Console.Clear();
				var graphics = state.Graphics;
				var frame = graphics.WorkFrame;
				frame.Clear();
				var playerColor = Color.Green;
				frame[point.X, point.Y] = playerColor;
				graphics.FrameBuffer.Swap();
				ConsoleRender(graphics.RenderFrame);
				var key = Console.ReadKey().Key;
				if ( key == ConsoleKey.Escape ) {
					abortSignal.IsAbortRequested = true;
					return;
				}
				point += Direction(key);
				var width = graphics.Screen.Width;
				var height = graphics.Screen.Height;
				point = FitToScreen(point, width, height);
			}
		}

		static void ConsoleRender(Frame frame) {
			Console.WriteLine();
			for ( var y = 0; y < 8; y++ ) {
				for ( var x = 0; x < 8; x++ ) {
					Console.BackgroundColor = FromColor(frame[x, y]);
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

		static Point2D Direction(ConsoleKey key) {
			switch ( key ) {
				case ConsoleKey.W: return new Point2D(0, -1);
				case ConsoleKey.S: return new Point2D(0, 1);
				case ConsoleKey.A: return new Point2D(-1, 0);
				case ConsoleKey.D: return new Point2D(1, 0);
			}
			return default;
		}

		static Point2D FitToScreen(Point2D point, int width, int height) {
			return new Point2D(Fit(point.X, 0, width), Fit(point.Y, 0, height));
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