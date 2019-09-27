using System;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SpawnSystem : ISystem {
		readonly SpawnRequestType        _request;
		readonly Action<Entity, Point2D> _spawnCallback;

		public SpawnSystem(SpawnRequestType request, Action<Entity, Point2D> spawnCallback) {
			_request       = request;
			_spawnCallback = spawnCallback;
		}

		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (_, position, ev) in state.Entities.Get<PositionComponent, SpawnEvent>() ) {
					if ( ev.Request != _request ) {
						continue;
					}
					_spawnCallback(editor.AddEntity(), position.Point);
				}
			}
		}
	}
}