using System.Collections.Generic;
using SimpleECS.Core.Components;
using SimpleECS.Core.Utils.Caching;

namespace SimpleECS.Core.Entities {
	public sealed class Entity {
		CacheScope _componentCache = null;

		List<IComponent> _components = new List<IComponent>();

		public T AddComponent<T>() where T : class, IComponent, new() {
			var instance = _componentCache.Hold<T>();
			_components.Add(instance);
			return instance;
		}

		public T AddComponent<T>(T instance) where T : class, IComponent, new() {
			_components.Add(instance);
			return instance;
		}

		public void RemoveComponent<T>(T component) where T : class, IComponent, new() {
			for ( var i = 0; i < _components.Count; i++ ) {
				if ( _components[i] == component ) {
					_components.RemoveAt(i);
					i--;
				}
			}
			_componentCache.Release(component);
		}

		public void RemoveComponent(object component) {
			for ( var i = 0; i < _components.Count; i++ ) {
				if ( _components[i] == component ) {
					_components.RemoveAt(i);
					i--;
				}
			}
			_componentCache.Release(component.GetType(), component);
		}

		public T GetComponent<T>() where T : class, IComponent {
			foreach ( var component in _components ) {
				if ( component is T tComponent ) {
					return tComponent;
				}
			}
			return null;
		}

		internal void Init(CacheScope componentCache) {
			_componentCache = componentCache;
		}

		internal void Reset() {
			foreach ( var component in _components ) {
				_componentCache.Release(component.GetType(), component);
			}
			_components.Clear();
		}
	}
}