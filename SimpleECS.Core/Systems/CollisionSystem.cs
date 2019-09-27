using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class CollisionSystem : ISystem {
		public void Update(GameState state) {
			var set = state.Entities.Get<PositionComponent, SolidBodyComponent>();
			foreach ( var (leftEntity, leftPosition, _) in set ) {
				foreach ( var (rightEntity, rightPosition, _) in set ) {
					if ( leftEntity == rightEntity ) {
						continue;
					}
					if ( leftPosition.Point != rightPosition.Point ) {
						continue;
					}
					leftEntity.AddComponent(new CollisionEvent(rightEntity));
				}
			}
		}
	}
}