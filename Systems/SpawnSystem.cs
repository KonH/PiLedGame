using System;
using PiLedGame.Common;
using PiLedGame.Components;
using PiLedGame.Entities;
using PiLedGame.Events;
using PiLedGame.State;

namespace PiLedGame.Systems {
	public sealed class SpawnSystem : ISystem {
		readonly string                  _requestId;
		readonly Action<Entity, Point2D> _spawnCallback;

		public SpawnSystem(string requestId, Action<Entity, Point2D> spawnCallback) {
			_requestId     = requestId;
			_spawnCallback = spawnCallback;
		}

		public void Update(GameState state) {
			using ( var editor = state.Entities.Edit() ) {
				foreach ( var (_, position, ev) in state.Entities.Get<PositionComponent, SpawnEvent>() ) {
					if ( ev.RequestId != _requestId ) {
						continue;
					}
					_spawnCallback(editor.AddEntity(), position.Point);
				}
			}
		}
	}
}