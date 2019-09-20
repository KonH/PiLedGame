using System.Collections.Generic;
using PiLedGame.Components;

namespace PiLedGame.Entities {
	public sealed class Entity {
		List<IComponent> _components = new List<IComponent>();

		public void AddComponent(IComponent component) {
			_components.Add(component);
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