using System;
using System.Drawing;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Components;

namespace ShootGame.Logic.Systems {
	public class RenderPlayerHealthSystem : ISystem {
		readonly Point2D          _startPosition;
		readonly Point2D          _offset;
		readonly Func<int, Color> _healthToColor;

		public RenderPlayerHealthSystem(Point2D startPosition, Point2D offset, Func<int, Color> healthToColor) {
			_startPosition = startPosition;
			_offset        = offset;
			_healthToColor = healthToColor;
		}

		public void Update(GameState state) {
			var health = GetHealth(state);
			RenderHealth(state, health);
		}

		int GetHealth(GameState state) {
			foreach ( var (_, _, health) in state.Entities.Get<PlayerComponent, HealthComponent>() ) {
				return health.Health;
			}
			return 0;
		}

		void RenderHealth(GameState state, int health) {
			var color = _healthToColor(health);
			for ( var i = 0; i < health; i++ ) {
				var position = _startPosition + new Point2D(_offset.X * i, _offset.Y * i);
				state.Graphics.Frame.ChangeAt(position, color);
			}
		}
	}
}