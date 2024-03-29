using System;
using System.Collections.Generic;
using SimpleECS.Core.Components;
using SimpleECS.Core.Utils.Caching;

namespace SimpleECS.Core.Entities {
	public sealed class EntitySet {
		List<Entity> _entities = new List<Entity>(128);
		EntityEditor _editor   = new EntityEditor();

		Cache<Entity> _entityCache    = null;
		CacheScope    _componentCache = null;
		CacheScope    _getCache       = null;

		public EntitySet() {
			_entityCache    = new Cache<Entity>();
			_componentCache = new CacheScope();
			_getCache       = new CacheScope();
		}

		public EntitySet(Cache<Entity> entityCache, CacheScope componentCache, CacheScope getCache) {
			_entityCache    = entityCache;
			_componentCache = componentCache;
			_getCache       = getCache;
		}

		public EntityEditor Edit() {
			_editor.Reset(_entities, _entityCache, _componentCache);
			return _editor;
		}

		public Entity Add() {
			using var editor = Edit();
			return editor.AddEntity();
		}

		public EntityCollection GetAll() {
			return new EntityCollection(_entities, _getCache);
		}

		public EntityComponentCollection<T1> Get<T1>()
			where T1 : class, IComponent {
			return new EntityComponentCollection<T1>(_entities, _getCache);
		}

		public ComponentCollection<T1> GetComponent<T1>()
			where T1 : class, IComponent {
			return new ComponentCollection<T1>(_entities, _getCache);
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

		public EntityComponentCollection<T1, T2> Get<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			return new EntityComponentCollection<T1, T2>(_entities, _getCache);
		}

		public ComponentCollection<T1, T2> GetComponent<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			return new ComponentCollection<T1, T2>(_entities, _getCache);
		}

		public ComponentCollection<T1, T2, T3> GetComponent<T1, T2, T3>()
			where T1 : class, IComponent
			where T2 : class, IComponent
			where T3 : class, IComponent {
			return new ComponentCollection<T1, T2, T3>(_entities, _getCache);
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

		public EntityComponentCollection<T1, T2, T3> Get<T1, T2, T3>()
			where T1 : class, IComponent
			where T2 : class, IComponent
			where T3 : class, IComponent {
			return new EntityComponentCollection<T1, T2, T3>(_entities, _getCache);
		}

		public void ReleaseGetCache() {
			_getCache.ReleaseAll();
		}

		public Dictionary<Type, int>[] GetDebugCacheInfo() {
			var result = new [] {
				_getCache.GetTotalCounts(),
				new Dictionary<Type, int> { { typeof(Entity), _editor.EntityCache.GetTotalCount() } },
				_editor.ComponentCache.GetTotalCounts(),
			};
			return result;
		}
	}
}