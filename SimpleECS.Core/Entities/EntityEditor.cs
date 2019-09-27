using System;
using System.Collections.Generic;

namespace SimpleECS.Core.Entities {
	public sealed class EntityEditor : IDisposable {
		List<Entity> _entities        = new List<Entity>();
		List<Entity> _newEntities     = new List<Entity>();
		List<Entity> _removedEntities = new List<Entity>();

		public void Reset(List<Entity> entities) {
			_entities = entities;
			_newEntities.Clear();
			_removedEntities.Clear();
		}

		public Entity AddEntity() {
			var entity = new Entity();
			_newEntities.Add(entity);
			return entity;
		}

		public void RemoveEntity(Entity entity) {
			_removedEntities.Add(entity);
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