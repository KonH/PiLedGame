using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SimpleECS.Core.Systems;
using ShootGame.Logic;
using ShootGame.Logic.Systems;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;
using KeyCode = SimpleECS.Core.Common.KeyCode;

namespace ShootGame.Unity {
	public class GameRunner : MonoBehaviour {
		enum State {
			Waiting,
			Create,
			Init,
			Update,
			Finish,
			Disable,
		}

		[SerializeField] float         StartDelay      = 0.0f;
		[SerializeField] float         EndDelay        = 0.0f;
		[SerializeField] string        ReplayPath      = null;
		[SerializeField] int           FramesPerUpdate = 1;
		[SerializeField] int           RandomSeed      = 0;
		[SerializeField] double        FixedFrameTime  = 0.0005;
		[SerializeField] bool          ReplayRecord    = false;
		[SerializeField] bool          ReplayShow      = false;
		[SerializeField] bool          ShowCacheCounts = false;
		[SerializeField] UnityRenderer Renderer        = null;

		EntitySet _entities;
		SystemSet _systems;
		State     _state;
		float     _endTime;

		void Update() {
			switch ( _state ) {
				case State.Waiting: {
					if ( Time.realtimeSinceStartup > StartDelay ) {
						_state = State.Create;
					}
				}
				break;

				case State.Create: {
					Profiler.BeginSample("Create");
					Create();
					Profiler.EndSample();
					_state = State.Init;
				}
				break;

				case State.Init: {
					Profiler.BeginSample("Init");
					Init();
					Profiler.EndSample();
					_state = State.Update;
				}
				break;

				case State.Update: {
					Profiler.BeginSample("UpdateFrames");
					UpdateFrames();
					Profiler.EndSample();
				}
				break;

				case State.Finish: {
					if ( Time.realtimeSinceStartup > (_endTime + EndDelay) ) {
						_state = State.Disable;
					}
				}
				break;

				case State.Disable: {
					_state = State.Disable;
					enabled = false;
					if ( ShowCacheCounts ) {
						DebugCacheCounts();
					}
					Debug.Break();
				}
				break;
			}
		}

		void Create() {
			InputRecordConfig replayRecord = null;
			if ( ReplayShow ) {
				replayRecord = new InputRecordConfig(ReplayPath);
			}
			var screen = new ScreenConfig(8, 8);
			var config = new Configuration(ReplayRecord, replayRecord, null, RandomSeed, FixedFrameTime);
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

		void Init() {
			_systems.Init(_entities);
		}

		void UpdateFrames() {
			for ( var i = 0; i < FramesPerUpdate; i++ ) {
				if ( UpdateOneFrame() ) {
					return;
				}
			}
		}

		bool UpdateOneFrame() {
			var isFinished = _systems.UpdateOnce(_entities);
			if ( isFinished ) {
				_systems.TryDispose();
				Debug.Log($"Your score is: {_systems.Get<ScoreMeasureSystem>()?.TotalScore}");
				Debug.Log($"Time: {_entities.GetFirstComponent<TimeState>().UnscaledTotalTime:0.00}");
				if ( ReplayRecord ) {
					var state = _entities.GetFirstComponent<InputRecordState>();
					state.Save(ReplayPath);
				}
				_state = State.Finish;
				_endTime = Time.realtimeSinceStartup;
			}
			return isFinished;
		}

		void DebugCacheCounts() {
			var sb = new StringBuilder();
			var data = _entities.GetDebugCacheInfo();
			foreach ( var item in data ) {
				foreach ( var pair in item ) {
					sb.AppendFormat("{0} = {1}\n", pair.Key.FullName, pair.Value);
				}
			}
			Debug.Log(sb.ToString());
		}
	}
}
