using System.Collections.Generic;
using SimpleECS.Core.Events;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
	public sealed class AddToInventorySystem : ComponentSystem<InventoryComponent, AddItemEvent> {
		public override void Update(List<(InventoryComponent, AddItemEvent)> components) {
			foreach ( var (inv, ev) in components ) {
				inv.AddItem(ev.Item);
			}
		}
	}
}