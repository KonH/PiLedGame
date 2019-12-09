using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class NoHealthDestroySystem : EditableEntityComponentSystem<HealthComponent> {
		public override void Update(EntityComponentCollection<HealthComponent> entities, EntityEditor editor) {
			foreach ( var (entity, health) in entities ) {
				if ( health.Health <= 0 ) {
					editor.RemoveEntity(entity);
				}
			}
		}
	}
}