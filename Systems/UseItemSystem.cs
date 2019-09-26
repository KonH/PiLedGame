using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class UseItemSystem<T> : ISystem where T : IEvent, new() {
		readonly string _type;

		public UseItemSystem(string type) {
			_type  = type;
		}

		public void Update(GameState state) {
			foreach ( var (entity, inv) in state.Entities.Get<InventoryComponent>() ) {
				if ( inv.TryGetItem(_type) ) {
					entity.AddComponent(new T());
				}
			}
		}
	}
}