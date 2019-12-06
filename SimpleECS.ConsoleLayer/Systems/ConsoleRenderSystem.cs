using System;
using SimpleECS.Core.Common;
using SimpleECS.Core.Configs;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ConsoleRenderSystem : IndependentSingleComponentSystem<DebugState, FrameState> {
		readonly ScreenConfig _screen;

		public ConsoleRenderSystem(ScreenConfig screen) {
			_screen = screen;
		}

		public override void Update(DebugState debug, FrameState frame) {
			if ( (debug != null) && !debug.IsTriggered ) {
				return;
			}
			for ( var y = _screen.Height - 1; y >= 0; y-- ) {
				for ( var x = 0; x < _screen.Width; x++ ) {
					Console.BackgroundColor = FromColor(frame[x, y]);
					Console.Write(" x ");
					Console.BackgroundColor = ConsoleColor.Black;
				}
				Console.WriteLine();
			}
		}

		static ConsoleColor FromColor(Color c) {
			var index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
			index |= (c.R > 64) ? 4 : 0; // Red bit
			index |= (c.G > 64) ? 2 : 0; // Green bit
			index |= (c.B > 64) ? 1 : 0; // Blue bit
			return (ConsoleColor)index;
		}
	}
}