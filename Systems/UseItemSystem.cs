using System;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class UseItemSystem : ISystem {
		readonly string         _type;
		readonly Action<Entity> _onUse;

		public UseItemSystem(string type, Action<Entity> onUse) {
			_type  = type;
			_onUse = onUse;
		}

		public void Update(GameState state) {
			foreach ( var (entity, inv) in state.Entities.Get<InventoryComponent>() ) {
				if ( inv.TryGetItem(_type) ) {
					_onUse(entity);
				}
			}
		}
	}
}