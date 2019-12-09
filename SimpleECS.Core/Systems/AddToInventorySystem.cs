using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class AddToInventorySystem : ComponentSystem<InventoryComponent, AddItemEvent> {
		public override void Update(ComponentCollection<InventoryComponent, AddItemEvent> components) {
			foreach ( var (inv, ev) in components ) {
				inv.AddItem(ev.Item);
			}
		}
	}
}