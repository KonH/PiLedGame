using System;
using System.Collections.Generic;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class PreventHealthSpawnSystem : ISystem {
		readonly SpawnRequestType _request;
		readonly int              _minHealth;

		public PreventHealthSpawnSystem(SpawnRequestType request, int minHealth) {
			_request   = request;
			_minHealth = minHealth;
		}

		public void Update(GameState state) {
			if ( IsHealthLimitReached(state.Entities.Get<PlayerComponent, HealthComponent>()) ) {
				return;
			}
			foreach ( var (entity, ev) in state.Entities.Get<SpawnEvent>() ) {
				if ( ev.Request == _request ) {
					entity.RemoveComponent(ev);
				}
			}
		}

		bool IsHealthLimitReached(List<ValueTuple<Entity, PlayerComponent, HealthComponent>> players) {
			foreach ( var (_, _, health) in players ) {
				if ( health.Health < _minHealth ) {
					return true;
				}
			}
			return false;
		}
	}
}