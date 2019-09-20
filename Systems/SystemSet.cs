using System;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SystemSet {
		readonly ISystem[] _systems;

		public SystemSet(params ISystem[] systems) {
			_systems = systems;
		}

		public void UpdateLoop(GameState state) {
			var isFinished = false;
			while ( true ) {
				try {
					Update(state);
					if ( state.Execution.IsFinished ) {
						TryDispose();
						isFinished = true;
					}
				} catch ( Exception e ) {
					state.Debug.Log(e.ToString());
				}
				if ( isFinished ) {
					break;
				}
			}
		}

		void Update(GameState state) {
			foreach ( var system in _systems ) {
				system.Update(state);
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