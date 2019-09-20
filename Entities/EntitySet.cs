using System.Collections.Generic;

namespace PiLedGame.Entities {
	public sealed class EntitySet {
		public IReadOnlyCollection<Entity> All => _entities;

		List<Entity> _entities        = new List<Entity>();
		List<Entity> _newEntities     = new List<Entity>();
		List<Entity> _removedEntities = new List<Entity>();

		public Entity AddEntity() {
			var entity = new Entity();
			_newEntities.Add(entity);
			return entity;
		}

		public void FlushNewEntities() {
			foreach ( var entity in _newEntities ) {
				_entities.Add(entity);
			}
			_newEntities.Clear();
		}

		public void RemoveEntity(Entity entity) {
			_removedEntities.Add(entity);
		}

		public void FlushRemovedEntities() {
			foreach ( var entity in _removedEntities ) {
				_entities.Remove(entity);
			}
			_removedEntities.Clear();
		}
	}
}