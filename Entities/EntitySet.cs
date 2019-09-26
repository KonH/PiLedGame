using System;
using System.Collections.Generic;
using PiLedGame.Components;

namespace PiLedGame.Entities {
	public sealed class EntitySet {
		List<Entity> _entities = new List<Entity>();
		EntityEditor _editor = new EntityEditor();

		public EntityEditor Edit() {
			_editor.Reset(_entities);
			return _editor;
		}

		public List<Entity> Get() {
			return new List<Entity>(_entities);
		}

		public List<ValueTuple<Entity, T1>> Get<T1>()
			where T1 : class, IComponent {
			var result = new List<ValueTuple<Entity, T1>>();
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				if ( c1 == null ) {
					continue;
				}
				result.Add((entity, c1));
			}
			return result;
		}

		public List<ValueTuple<Entity, T1, T2>> Get<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			var result = new List<ValueTuple<Entity, T1, T2>>();
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				var c2 = entity.GetComponent<T2>();
				if ( (c1 == null) || (c2 == null) ) {
					continue;
				}
				result.Add((entity, c1, c2));
			}
			return result;
		}

		public List<ValueTuple<Entity, T1, T2, T3>> Get<T1, T2, T3>()
			where T1 : class, IComponent
			where T2 : class, IComponent
			where T3 : class, IComponent {
			var result = new List<ValueTuple<Entity, T1, T2, T3>>();
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				var c2 = entity.GetComponent<T2>();
				var c3 = entity.GetComponent<T3>();
				if ( (c1 == null) || (c2 == null) || (c3 == null) ) {
					continue;
				}
				result.Add((entity, c1, c2, c3));
			}
			return result;
		}
	}
}