using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class OutOfBoundsDestroySystem : EntityComponentSystem<OutOfBoundsDestroyComponent, PositionComponent> {
		readonly ScreenConfig _screen;

		public OutOfBoundsDestroySystem(ScreenConfig screen) {
			_screen = screen;
		}

		public override void Update(Entity entity, OutOfBoundsDestroyComponent _, PositionComponent position) {
			var width = _screen.Width;
			var height = _screen.Height;
			var point = position.Point;
			if ( (point.X < 0) || (point.X >= width) || (point.Y < 0) || (point.Y >= height) ) {
				entity.AddComponent<DestroyEvent>();
			}
		}
	}
}