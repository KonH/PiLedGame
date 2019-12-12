using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class AddTrailRenderSystem : EditableComponentSystem<PositionComponent, TrailComponent, MovementEvent> {
		readonly double _decrease;

		public AddTrailRenderSystem(double decrease) {
			_decrease = decrease;
		}

		public override void Update(ComponentCollection<PositionComponent, TrailComponent, MovementEvent> entities, EntityEditor editor) {
			foreach ( var (pos, trail, mov) in entities ) {
				var oldPosition = pos.Point - mov.Offset;
				var color = trail.Color;
				var entity = editor.AddEntity();
				entity.AddComponent<PositionComponent>().Init(oldPosition);
				entity.AddComponent<RenderComponent>().Init(color);
				entity.AddComponent<FadeRenderComponent>().Init(_decrease);
			}
		}
	}
}