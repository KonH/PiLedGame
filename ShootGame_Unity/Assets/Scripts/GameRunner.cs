using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleECS.Core.State;
using SimpleECS.Core.Systems;
using ShootGame.Logic;
using ShootGame.Logic.Systems;

using Debug = UnityEngine.Debug;
using KeyCode = SimpleECS.Core.Common.KeyCode;

namespace ShootGame.Unity {
	public class GameRunner : MonoBehaviour {
		[SerializeField] UnityRenderer Renderer = null;

		GameState _state;
		SystemSet _systems;

		void Awake() {
			var config = new Configuration(false, null, null, 0);
			var root = new CompositionRoot(
				config,
				() => new ReadUnityInputSystem(new Dictionary<UnityEngine.KeyCode, KeyCode> {
					{ UnityEngine.KeyCode.LeftArrow, KeyCode.LeftArrow },
					{ UnityEngine.KeyCode.RightArrow, KeyCode.RightArrow },
					{ UnityEngine.KeyCode.Space, KeyCode.Spacebar }
				}),
				new Func<ISystem>[0],
				() => new ReadUnityRealTimeSystem(),
				() => new UnityRenderSystem(Renderer));
			(_state, _systems) = root.Create();
		}

		void Start() {
			_systems.Init(_state);
		}

		void Update() {
			var isFinished = _systems.UpdateOnce(_state);
			if ( isFinished ) {
				_systems.TryDispose();
				Debug.Log($"Your score is: {_systems.Get<ScoreMeasureSystem>()?.TotalScore}");
				Debug.Log($"Time: {_state.Time.UnscaledTotalTime:0.00}");
				enabled = false;
			}
		}
	}
}
