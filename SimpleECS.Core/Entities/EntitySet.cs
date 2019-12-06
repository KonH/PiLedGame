using System;
using System.Collections.Generic;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Entities {
	public sealed class EntitySet {
		List<Entity> _entities = new List<Entity>();
		EntityEditor _editor = new EntityEditor();

		public EntityEditor Edit() {
			_editor.Reset(_entities);
			return _editor;
		}

		public Entity Add() {
			using var editor = Edit();
			return editor.AddEntity();
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

		public List<T1> GetComponent<T1>()
			where T1 : class, IComponent {
			var result = new List<T1>();
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				if ( c1 == null ) {
					continue;
				}
				result.Add(c1);
			}
			return result;
		}

		public ValueTuple<Entity, T1> GetFirst<T1>()
			where T1 : class, IComponent {
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				if ( c1 == null ) {
					continue;
				}
				return (entity, c1);
			}
			return default;
		}

		public T1 GetFirstComponent<T1>()
			where T1 : class, IComponent {
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				if ( c1 == null ) {
					continue;
				}
				return c1;
			}
			return default;
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

		public List<ValueTuple<T1, T2>> GetComponent<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			var result = new List<ValueTuple<T1, T2>>();
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				var c2 = entity.GetComponent<T2>();
				if ( (c1 == null) || (c2 == null) ) {
					continue;
				}
				result.Add((c1, c2));
			}
			return result;
		}

		public ValueTuple<T1, T2> GetFirstComponent<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			foreach ( var entity in _entities ) {
				var c1 = entity.GetComponent<T1>();
				var c2 = entity.GetComponent<T2>();
				if ( (c1 == null) || (c2 == null) ) {
					continue;
				}
				return (c1, c2);
			}
			return default;
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