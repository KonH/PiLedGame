using System;
using System.Collections.Generic;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Entities {
	public sealed class Entity {
		List<IComponent> _components = new List<IComponent>();

		public void AddComponent(IComponent component) {
			_components.Add(component);
		}

		public void RemoveComponent(IComponent component) {
			_components.Remove(component);
		}

		public void RemoveComponent(Predicate<IComponent> condition) {
			_components.RemoveAll(condition);
		}

		public T GetComponent<T>() where T : class, IComponent {
			foreach ( var component in _components ) {
				if ( component is T tComponent ) {
					return tComponent;
				}
			}
			return null;
		}
	}
}