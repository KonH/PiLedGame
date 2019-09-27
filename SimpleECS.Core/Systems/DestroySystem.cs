using SimpleECS.Core.State;
using SimpleECS.Core.Events;

namespace SimpleECS.Core.Systems {
	public sealed class DestroySystem : ISystem {
		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (entity, _) in state.Entities.Get<DestroyEvent>() ) {
					editor.RemoveEntity(entity);
				}
			}
		}
	}
}