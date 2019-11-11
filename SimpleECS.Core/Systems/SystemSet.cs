using System;
using System.Collections.Generic;
using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public sealed class SystemSet {
		readonly List<ISystem> _systems = new List<ISystem>();

		public void Add(ISystem system) {
			_systems.Add(system);
		}

		public T Get<T>() where T : ISystem {
			return (T)_systems.Find(s => s is T);
		}

		public void Init(GameState state) {
			foreach ( var system in _systems ) {
				var init = system as IInit;
				init?.Init(state);
			}
		}

		public void UpdateLoop(GameState state) {
			while ( true ) {
				var isFinished = UpdateOnce(state);
				if ( isFinished ) {
					TryDispose();
					break;
				}
			}
		}

		public bool UpdateOnce(GameState state) {
			Update(state);
			if ( state.Execution.IsFinished ) {
				TryDispose();
				return true;
			}
			return false;
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

		public void TryDispose() {
			foreach ( var system in _systems ) {
				if ( system is IDisposable disposable ) {
					disposable.Dispose();
				}
			}
		}
	}
}