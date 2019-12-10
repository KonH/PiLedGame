using System;
using System.Collections;
using System.Collections.Generic;
using SimpleECS.Core.Components;
using SimpleECS.Core.Utils.Caching;

namespace SimpleECS.Core.Entities {
	public struct ComponentCollection<T1> : IEnumerable<T1>
		where T1 : class, IComponent {
		sealed class Enumerator : IEnumerator<T1> {
			List<Entity> _entities;

			int _index;

			public void Init(List<Entity> entities) {
				_entities = entities;
				_index    = -1;

				Current = null;
			}

			public bool MoveNext() {
				Current = null;
				while ( Current == null ) {
					_index++;
					if ( _index >= _entities.Count ) {
						return false;
					}
					var entity = _entities[_index];
					Current = entity.GetComponent<T1>();
				}
				return true;
			}

			public void Reset() {
				_index = -1;
			}

			public T1 Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() {}
		}

		readonly List<Entity> _entities;
		readonly CacheScope   _cache;

		internal ComponentCollection(List<Entity> entities, CacheScope cache) {
			_entities = entities;
			_cache    = cache;
		}

		public IEnumerator<T1> GetEnumerator() {
			var iter = _cache.Hold<Enumerator>();
			iter.Init(_entities);
			return iter;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	public struct ComponentCollection<T1, T2> : IEnumerable<ValueTuple<T1, T2>>
		where T1 : class, IComponent
		where T2 : class, IComponent {
		sealed class Enumerator : IEnumerator<ValueTuple<T1, T2>> {
			List<Entity> _entities;

			int _index;

			public void Init(List<Entity> entities) {
				_entities = entities;
				_index    = -1;

				Current = default;
			}

			public bool MoveNext() {
				Current = default;
				while ( Current == default ) {
					_index++;
					if ( _index >= _entities.Count ) {
						return false;
					}
					var entity = _entities[_index];
					var c1 = entity.GetComponent<T1>();
					if ( c1 == null ) {
						continue;
					}
					var c2 = entity.GetComponent<T2>();
					if ( c2 == null ) {
						continue;
					}
					Current = (c1, c2);
				}
				return true;
			}

			public void Reset() {
				_index = -1;
			}

			public ValueTuple<T1, T2> Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() {}
		}

		readonly List<Entity> _entities;
		readonly CacheScope   _cache;

		internal ComponentCollection(List<Entity> entities, CacheScope cache) {
			_entities = entities;
			_cache    = cache;
		}

		public IEnumerator<ValueTuple<T1, T2>> GetEnumerator() {
			var iter = _cache.Hold<Enumerator>();
			iter.Init(_entities);
			return iter;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}