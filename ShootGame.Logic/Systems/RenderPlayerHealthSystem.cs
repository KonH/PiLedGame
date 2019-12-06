using ShootGame.Logic.Configs;
using SimpleECS.Core.Common;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace ShootGame.Logic.Systems {
	public class RenderPlayerHealthSystem : ISystem {
		readonly RenderPlayerHealthConfig _config;

		public RenderPlayerHealthSystem(RenderPlayerHealthConfig config) {
			_config = config;
		}

		public void Update(EntitySet entities) {
			var (_, health) = entities.GetFirstComponent<PlayerComponent, HealthComponent>();
			var value = health?.Health ?? 0;
			foreach ( var frame in entities.GetComponent<FrameState>()) {
				RenderHealth(frame, value);
			}
		}

		void RenderHealth(FrameState frame, int health) {
			var color = _config.HealthToColor(health);
			for ( var i = 0; i < health; i++ ) {
				var offset = new Point2D(_config.Offset.X * i, _config.Offset.Y * i);
				var position = _config.StartPosition + offset;
				frame.ChangeAt(position, color);
			}
		}
	}
}