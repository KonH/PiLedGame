using System;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class KeyboardSpawnSystem : ISystem {
		readonly ConsoleKey       _key;
		readonly SpawnRequestType _request;

		public KeyboardSpawnSystem(ConsoleKey key, SpawnRequestType request) {
			_key     = key;
			_request = request;
		}

		public void Update(GameState state) {
			var key = state.Input.Current;
			foreach ( var (entity, spawn, _) in state.Entities.Get<SpawnComponent, KeyboardSpawnComponent>() ) {
				if ( (key == _key) && (_request == spawn.Request) ) {
					entity.AddComponent(new SpawnEvent(spawn.Request));
				}
			}
		}
	}
}