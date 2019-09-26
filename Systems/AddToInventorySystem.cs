using PiLedGame.Components;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class AddToInventorySystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, inv, ev) in state.Entities.Get<InventoryComponent, AddItemEvent>() ) {
				inv.AddItem(ev.Type, ev.Count);
			}
		}
	}
}