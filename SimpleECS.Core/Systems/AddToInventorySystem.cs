using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class AddToInventorySystem : ComponentSystem<InventoryComponent, AddItemEvent> {
		public override void Update(InventoryComponent inventory, AddItemEvent ev) {
			inventory.AddItem(ev.Item);
		}
	}
}