using System;
using System.Collections.Generic;
using SimpleECS.Core.Utils.Caching;

namespace SimpleECS.Core.Entities {
	public sealed class EntityEditor : IDisposable {
		Cache<Entity> _entityCache    = null;
		CacheScope    _componentCache = null;

		List<Entity> _entities        = new List<Entity>();
		List<Entity> _newEntities     = new List<Entity>();
		List<Entity> _removedEntities = new List<Entity>();

		internal Cache<Entity> EntityCache    => _entityCache;
		internal CacheScope    ComponentCache => _componentCache;

		public void Reset(List<Entity> entities, Cache<Entity> entityCache, CacheScope componentCache) {
			_entities       = entities;
			_entityCache    = entityCache;
			_componentCache = componentCache;
			_newEntities.Clear();
			_removedEntities.Clear();
		}

		public Entity AddEntity() {
			var entity = _entityCache.Hold();
			entity.Init(_componentCache);
			_newEntities.Add(entity);
			return entity;
		}

		public void RemoveEntity(Entity entity) {
			_removedEntities.Add(entity);
			entity.Reset();
			_entityCache.Release(entity);
		}

		public void Dispose() {
			foreach ( var entity in _newEntities ) {
				_entities.Add(entity);
			}
			_newEntities.Clear();
			foreach ( var entity in _removedEntities ) {
				_entities.Remove(entity);
			}
			_removedEntities.Clear();
		}
	}
}