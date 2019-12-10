using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class CollisionSystem : EntityComponentSystem<PositionComponent, SolidBodyComponent> {
		public override void Update(EntityComponentCollection<PositionComponent, SolidBodyComponent> entities) {
			foreach ( var (leftEntity, leftPosition, _) in entities ) {
				foreach ( var (rightEntity, rightPosition, _) in entities ) {
					if ( leftEntity == rightEntity ) {
						continue;
					}
					if ( leftPosition.Point != rightPosition.Point ) {
						continue;
					}
					leftEntity.AddComponent<CollisionEvent>().Init(rightEntity);
				}
			}
		}
	}
}