using SimpleECS.Core.Events;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public sealed class UseItemSystem<T> : EntityComponentSystem<InventoryComponent> where T : class, IEvent, new() {
		readonly UseItemConfig _config;

		public UseItemSystem(UseItemConfig config) {
			_config = config;
		}

		public override void Update(Entity entity, InventoryComponent inventory) {
			if ( inventory.TryGetItem(_config.Item) ) {
				entity.AddComponent<T>();
			}
		}
	}
}