using System.Collections.Generic;

namespace PiLedGame.Entities {
	public sealed class EntitySet {
		public IReadOnlyCollection<Entity> All => _entities;

		List<Entity> _entities = new List<Entity>();

		public Entity AddEntity() {
			var entity = new Entity();
			_entities.Add(entity);
			return entity;
		}
	}
}