using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleECS.Core.Systems;
using ShootGame.Logic;
using ShootGame.Logic.Systems;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

using Debug = UnityEngine.Debug;
using KeyCode = SimpleECS.Core.Common.KeyCode;

namespace ShootGame.Unity {
	public class GameRunner : MonoBehaviour {
		[SerializeField] UnityRenderer Renderer = null;

		EntitySet _entities;
		SystemSet _systems;

		void Awake() {
			var screen = new ScreenConfig(8, 8);
			var config = new Configuration(false, null, null, 0);
			var root = new CompositionRoot(
				screen,
				null,
				config,
				() => new ReadUnityInputSystem(new Dictionary<UnityEngine.KeyCode, KeyCode> {
					{ UnityEngine.KeyCode.LeftArrow, KeyCode.LeftArrow },
					{ UnityEngine.KeyCode.RightArrow, KeyCode.RightArrow },
					{ UnityEngine.KeyCode.Space, KeyCode.Spacebar }
				}),
				new Func<ISystem>[0],
				() => new ReadUnityRealTimeSystem(),
				() => new UnityRenderSystem(screen, Renderer));
			(_entities, _systems) = root.Create();
		}

		void Start() {
			_systems.Init(_entities);
		}

		void Update() {
			var isFinished = _systems.UpdateOnce(_entities);
			if ( isFinished ) {
				_systems.TryDispose();
				Debug.Log($"Your score is: {_systems.Get<ScoreMeasureSystem>()?.TotalScore}");
				Debug.Log($"Time: {_entities.GetFirstComponent<TimeState>().UnscaledTotalTime:0.00}");
				enabled = false;
			}
		}
	}
}
