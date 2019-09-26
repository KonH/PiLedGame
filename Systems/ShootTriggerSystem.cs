using System;
using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class KeyboardSpawnSystem : ISystem {
		readonly ConsoleKey _key;
		readonly string     _requestId;

		public KeyboardSpawnSystem(ConsoleKey key, string requestId) {
			_key       = key;
			_requestId = requestId;
		}

		public void Update(GameState state) {
			var key = state.Input.Current;
			foreach ( var (entity, spawn, _) in state.Entities.Get<SpawnComponent, KeyboardSpawnComponent>() ) {
				if ( key == _key ) {
					entity.AddComponent(new SpawnEvent(spawn.RequestId));
				}
			}
		}
	}
}