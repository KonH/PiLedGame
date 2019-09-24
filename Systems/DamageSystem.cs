using System.Collections.Generic;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class DamageSystem : ISystem {
		Dictionary<Point2D, List<DamageComponent>> _damageByPosition = new Dictionary<Point2D, List<DamageComponent>>();

		public void Update(GameState state) {
			_damageByPosition.Clear();
			foreach ( var (_, position, damage) in state.Entities.Get<PositionComponent, DamageComponent>() ) {
				if ( !_damageByPosition.TryGetValue(position.Point, out var damages) ) {
					damages = new List<DamageComponent>();
					_damageByPosition.Add(position.Point, damages);
				}
				damages.Add(damage);
			}
			foreach ( var (_, position, health) in state.Entities.Get<PositionComponent, HealthComponent>() ) {
				if ( !_damageByPosition.TryGetValue(position.Point, out var damages) ) {
					continue;
				}
				foreach ( var damage in damages ) {
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