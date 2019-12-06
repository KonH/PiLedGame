using System;
using System.Collections.Generic;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class SystemSet {
		readonly List<ISystem> _systems = new List<ISystem>();

		public void Add(ISystem system) {
			_systems.Add(system);
		}

		public T Get<T>() where T : ISystem {
			return (T)_systems.Find(s => s is T);
		}

		public void Init(EntitySet entities) {
			foreach ( var system in _systems ) {
				var init = system as IInit;
				init?.Init(entities);
			}
		}

		public void UpdateLoop(EntitySet entities) {
			while ( true ) {
				var isFinished = UpdateOnce(entities);
				if ( isFinished ) {
					TryDispose();
					break;
				}
			}
		}

		public bool UpdateOnce(EntitySet entities) {
			Update(entities);
			var exec = entities.GetFirstComponent<ExecutionState>();
			if ( (exec != null) && exec.IsFinished ) {
				TryDispose();
				return true;
			}
			return false;
		}

		void Update(EntitySet entities) {
			foreach ( var system in _systems ) {
				try {
					system.Update(entities);
				}
				catch ( Exception e ) {
					var debug = entities.GetFirstComponent<DebugState>();
					if ( debug != null ) {
						debug.Log(e.ToString());
					} else {
						Console.Write(e.ToString());
					}
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