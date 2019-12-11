using System.Collections.Generic;
using SimpleECS.Core.Common;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;
using SimpleECS.Core.States;
using SimpleECS.Core.Utils.Caching;

namespace SimpleECS.Core.Systems {
	public sealed class TrailRenderSystem : ISystem {
		Cache<TrailData> _cache = new Cache<TrailData>(16);

		class TrailData {
			public List<PositionData> Positions = new List<PositionData>(8);

			Color  _color;
			double _wantedTime;

			public TrailData Init(Color color, double wantedTime) {
				_color      = color;
				_wantedTime = wantedTime;
				return this;
			}

			public void TryAdd(double time, Point2D point) {
				if ( (Positions.Count == 0) || (Positions[Positions.Count - 1].Point != point) ) {
					Positions.Add(new PositionData(time, point));
				}
			}

			public bool Trim(double time) {
				if ( Positions.Count == 0 ) {
					return false;
				}
				if ( Positions[0].Time < (time - _wantedTime) ) {
					Positions.RemoveAt(0);
				}
				return true;
			}

			public Color CalculateAt(double time, PositionData data) {
				var power = 1 - ((time - data.Time) / _wantedTime);
				return _color.Multiply(power);
			}
		}

		struct PositionData {
			public readonly double  Time;
			public readonly Point2D Point;

			public PositionData(double time, Point2D point) {
				Time  = time;
				Point = point;
			}
		}

		Dictionary<TrailComponent, TrailData> _data         = new Dictionary<TrailComponent, TrailData>(16);
		List<TrailComponent>                  _outdatedData = new List<TrailComponent>(16);

		public void Update(EntitySet entities) {
			var time = entities.GetFirstComponent<TimeState>().TotalTime;
			Update(time, entities);
			Trim(time);
			foreach ( var frame in entities.GetComponent<FrameState>() ) {
				Render(time, frame);
			}
		}

		void Update(double time, EntitySet entities) {
			foreach ( var (_, trail, position) in entities.Get<TrailComponent, PositionComponent>() ) {
				if ( !_data.TryGetValue(trail, out var data) ) {
					data = _cache.Hold().Init(trail.Color, trail.WantedTime);
					_data.Add(trail, data);
				}
				data.TryAdd(time, position.Point);
			}
		}

		void Trim(double time) {
			foreach ( var p in _data ) {
				var trail = p.Key;
				var data = p.Value;
				if ( !data.Trim(time) ) {
					_outdatedData.Add(trail);
				}
			}
			foreach ( var trail in _outdatedData ) {
				_cache.Release(_data[trail]);
				_data.Remove(trail);
			}
			_outdatedData.Clear();
		}

		void Render(double time, FrameState frame) {
			foreach ( var p in _data ) {
				var data = p.Value;
				foreach ( var pos in data.Positions ) {
					frame.ChangeAt(pos.Point, data.CalculateAt(time, pos));
				}
			}
		}
	}
}