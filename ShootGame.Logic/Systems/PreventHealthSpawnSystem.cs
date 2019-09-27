using System;
using System.Collections.Generic;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;

namespace ShootGame.Logic.Systems {
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