using System.Collections.Generic;
using System.Drawing;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class TrailRenderSystem : ISystem {
		struct PositionData {
			public readonly double  Time;
			public readonly Point2D Point;

			public PositionData(double time, Point2D point) {
				Time  = time;
				Point = point;
			}
		}

		readonly double _time;

		Dictionary<TrailComponent, List<PositionData>> _history = new Dictionary<TrailComponent, List<PositionData>>();

		public TrailRenderSystem(double time) {
			_time = time;
		}

		public void Update(GameState state) {
			var time = state.Time.TotalTime;
			var renderFrame = state.Graphics.Frame;
			foreach ( var (_, trail, position) in state.Entities.Get<TrailComponent, PositionComponent>() ) {
				if ( !_history.TryGetValue(trail, out var history) ) {
					history = new List<PositionData>();
					_history.Add(trail, history);
				}
				var point = position.Point;
				if ( (history.Count == 0) || (history[history.Count -1].Point != point) ) {
					history.Add(new PositionData(time, point));
				}
				if ( history[0].Time < (time - _time) ) {
					history.RemoveAt(0);
				}
				RenderTrail(time, renderFrame, trail.Color, history);
			}
		}

		void RenderTrail(double time, Frame frame, Color color, List<PositionData> history) {
			foreach ( var data in history ) {
				var position = data.Point;
				var power = 1 - ((time - data.Time) / _time);
				frame[position.X, position.Y] = Multiply(color, power);
			}
		}

		Color Multiply(Color color, double power) {
			return Color.FromArgb((byte)(color.R * power), (byte)(color.G * power), (byte)(color.B * power));
		}
	}
}