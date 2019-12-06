using System.Collections.Generic;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class DestroyCollectedItemSystem : EntityComponentSystem<CollectItemEvent> {
		public override void Update(List<(Entity, CollectItemEvent)> entities) {
			foreach ( var (entity, _) in entities ) {
				entity.AddComponent(new DestroyEvent());
			}
		}
	}
}