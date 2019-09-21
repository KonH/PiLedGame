using PiLedGame.Components;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SpawnSystem : ISystem {
		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (_, spawn, position) in state.Entities.Get<SpawnComponent, PositionComponent>() ) {
					if ( !spawn.ShouldSpawn ) {
						continue;
					}
					spawn.Factory(editor.AddEntity(), position.Point, spawn.Direction);
					spawn.ShouldSpawn = false;
				}
			}
		}
	}
}