using System.Collections.Generic;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class DamageSystem : ISystem {
		Dictionary<Point2D, DamageComponent> _damageByPosition = new Dictionary<Point2D, DamageComponent>();

		public void Update(GameState state) {
			_damageByPosition.Clear();
			foreach ( var (_, position, damage) in state.Entities.Get<PositionComponent, DamageComponent>() ) {
				_damageByPosition[position.Point] = damage;
			}
			foreach ( var (_, position, health) in state.Entities.Get<PositionComponent, HealthComponent>() ) {
				if ( _damageByPosition.TryGetValue(position.Point, out var damage) ) {
					if ( damage.Layer == health.Layer ) {
						continue;
					}
					health.Health -= damage.Damage;
					damage.Triggered = true;
				}
			}
		}
	}
}