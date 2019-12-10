using System;
using System.Collections;
using System.Collections.Generic;
using SimpleECS.Core.Components;
using SimpleECS.Core.Utils.Caching;

namespace SimpleECS.Core.Entities {
	public struct EntityCollection : IEnumerable<Entity> {
		public sealed class Enumerator : IEnumerator<Entity> {
			List<Entity> _entities;

			int _index;

			public void Init(List<Entity> entities) {
				_entities = entities;
				_index    = -1;

				Current = default;
			}

			public bool MoveNext() {
				Current = null;
				while ( Current == null ) {
					_index++;
					if ( _index >= _entities.Count ) {
						return false;
					}
					var entity = _entities[_index];
					Current = entity;
				}
				return true;
			}

			public void Reset() {
				_index = -1;
			}

			public Entity Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() {}
		}

		readonly List<Entity> _entities;
		readonly CacheScope   _cache;

		public int Count {
			get {
				var acc = 0;
				foreach ( var _ in this ) {
					acc++;
				}
				return acc;
			}
		}

		internal EntityCollection(List<Entity> entities, CacheScope cache) {
			_entities = entities;
			_cache    = cache;
		}

		public IEnumerator<Entity> GetEnumerator() {
			var iter = _cache.Hold<Enumerator>();
			iter.Init(_entities);
			return iter;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	public struct EntityComponentCollection<T1> : IEnumerable<(Entity, T1)>
		where T1 : class, IComponent {
		public sealed class Enumerator : IEnumerator<(Entity, T1)> {
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
					Current = (entity, c1);
				}
				return true;
			}

			public void Reset() {
				_index = -1;
			}

			public ValueTuple<Entity, T1> Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() {}
		}

		readonly List<Entity> _entities;
		readonly CacheScope   _cache;

		public int Count {
			get {
				var acc = 0;
				foreach ( var _ in this ) {
					acc++;
				}
				return acc;
			}
		}

		internal EntityComponentCollection(List<Entity> entities, CacheScope cache) {
			_entities = entities;
			_cache    = cache;
		}

		public IEnumerator<ValueTuple<Entity, T1>> GetEnumerator() {
			var iter = _cache.Hold<Enumerator>();
			iter.Init(_entities);
			return iter;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	public struct EntityComponentCollection<T1, T2> : IEnumerable<(Entity, T1, T2)>
		where T1 : class, IComponent
		where T2 : class, IComponent {
		public sealed class Enumerator : IEnumerator<(Entity, T1, T2)> {
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
					Current = (entity, c1, c2);
				}
				return true;
			}

			public void Reset() {
				_index = -1;
			}

			public ValueTuple<Entity, T1, T2> Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() {}
		}

		readonly List<Entity> _entities;
		readonly CacheScope   _cache;

		internal EntityComponentCollection(List<Entity> entities, CacheScope cache) {
			_entities = entities;
			_cache    = cache;
		}

		public IEnumerator<ValueTuple<Entity, T1, T2>> GetEnumerator() {
			var iter = _cache.Hold<Enumerator>();
			iter.Init(_entities);
			return iter;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	public struct EntityComponentCollection<T1, T2, T3> : IEnumerable<(Entity, T1, T2, T3)>
		where T1 : class, IComponent
		where T2 : class, IComponent
		where T3 : class, IComponent {
		public sealed class Enumerator : IEnumerator<(Entity, T1, T2, T3)> {
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
					var c3 = entity.GetComponent<T3>();
					if ( c3 == null ) {
						continue;
					}
					Current = (entity, c1, c2, c3);
				}
				return true;
			}

			public void Reset() {
				_index = -1;
			}

			public ValueTuple<Entity, T1, T2, T3> Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() {}
		}

		readonly List<Entity> _entities;
		readonly CacheScope   _cache;

		internal EntityComponentCollection(List<Entity> entities, CacheScope cache) {
			_entities = entities;
			_cache    = cache;
		}

		public IEnumerator<ValueTuple<Entity, T1, T2, T3>> GetEnumerator() {
			var iter = _cache.Hold<Enumerator>();
			iter.Init(_entities);
			return iter;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}