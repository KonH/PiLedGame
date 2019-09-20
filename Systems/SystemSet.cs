using System;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SystemSet {
		readonly ISystem[] _systems;

		public SystemSet(params ISystem[] systems) {
			_systems = systems;
		}

		public void UpdateLoop(GameState state) {
			while ( true ) {
				Update(state);
				if ( state.Execution.IsFinished ) {
					TryDispose();
					break;
				}
			}
		}

		void Update(GameState state) {
			foreach ( var system in _systems ) {
				try {
					system.Update(state);
				}
				catch ( Exception e ) {
					state.Debug.Log(e.ToString());
				}
			}
		}

		void TryDispose() {
			foreach ( var system in _systems ) {
				if ( system is IDisposable disposable ) {
					disposable.Dispose();
				}
			}
		}
	}
}