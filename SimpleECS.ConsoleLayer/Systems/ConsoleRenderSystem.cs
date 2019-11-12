using System;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Systems;

namespace SimpleECS.ConsoleLayer.Systems {
	public sealed class ConsoleRenderSystem : ISystem {
		public void Update(GameState state) {
			if ( !state.Debug.IsTriggered ) {
				return;
			}
			var frame = state.Graphics.Frame;
			for ( var y = 0; y < 8; y++ ) {
				for ( var x = 0; x < 8; x++ ) {
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