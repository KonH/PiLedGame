using System;
using SimpleECS.Core.State;
using SimpleECS.Core.Common;
using SimpleECS.Core.Events;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.Systems {
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