using System;
using PiLedGame.State;

namespace PiLedGame.System {
	public sealed class SystemSet {
		readonly ISystem[] _systems;

		public SystemSet(ISystem[] systems) {
			_systems = systems;
		}

		public void Update(GameState state) {
			foreach ( var system in _systems ) {
				system.Update(state);
			}
		}

		public void TryDispose() {
			foreach ( var system in _systems ) {
				if ( system is IDisposable disposable ) {
					disposable.Dispose();
				}
			}
		}
	}
}