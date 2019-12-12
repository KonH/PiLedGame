using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class CollisionSystem : ISystem {
		public void Update(EntitySet allEntities) {
			var entities = allEntities.Get<PositionComponent, SolidBodyComponent>();
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