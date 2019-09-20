using System.Collections.Generic;
using System.Drawing;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class TrailRenderSystem : ISystem {
		readonly int _frameDepth;

		Dictionary<TrailComponent, List<Point2D>> _history = new Dictionary<TrailComponent, List<Point2D>>();

		public TrailRenderSystem(int frameDepth) {
			_frameDepth = frameDepth;
		}

		public void Update(GameState state) {
			var renderFrame = state.Graphics.Frame;
			foreach ( var (_, trail, position) in state.Entities.Get<TrailComponent, PositionComponent>() ) {
				if ( !_history.TryGetValue(trail, out var history) ) {
					history = new List<Point2D>();
					_history.Add(trail, history);
				}
				history.Add(position.Point);
				if ( history.Count == _frameDepth ) {
					history.RemoveAt(0);
				}
				RenderTrail(renderFrame, trail.Color, history);
			}
		}

		void RenderTrail(Frame frame, Color color, List<Point2D> history) {
			for ( var i = 0; i < history.Count; i++ ) {
				var position = history[i];
				var power = (double)i / history.Count;
				frame[position.X, position.Y] = Multiply(color, power);
			}
		}

		Color Multiply(Color color, double power) {
			return Color.FromArgb((byte)(color.R * power), (byte)(color.G * power), (byte)(color.B * power));
		}
	}
}