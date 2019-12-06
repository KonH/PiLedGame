using System;
using System.Collections.Generic;
using ShootGame.Logic.Configs;
using SimpleECS.Core.Events;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;

namespace ShootGame.Logic.Systems {
	public sealed class PreventHealthSpawnSystem : ISystem {
		readonly PreventHealthSpawnConfig _config;

		public PreventHealthSpawnSystem(PreventHealthSpawnConfig config) {
			_config = config;
		}

		public void Update(EntitySet entities) {
			if ( IsHealthLimitReached(entities.GetComponent<PlayerComponent, HealthComponent>()) ) {
				return;
			}
			foreach ( var (entity, ev) in entities.Get<SpawnEvent>() ) {
				if ( ev.Request == _config.Request ) {
					entity.RemoveComponent(ev);
				}
			}
		}

		bool IsHealthLimitReached(List<ValueTuple<PlayerComponent, HealthComponent>> players) {
			foreach ( var (_, health) in players ) {
				if ( health.Health < _config.MinHealth ) {
					return true;
				}
			}
			return false;
		}
	}
}