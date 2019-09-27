using SimpleECS.Core.State;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class AddToInventorySystem : ISystem {
		public void Update(GameState state) {
			foreach ( var (_, inv, ev) in state.Entities.Get<InventoryComponent, AddItemEvent>() ) {
				inv.AddItem(ev.Item);
			}
		}
	}
}