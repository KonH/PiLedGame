﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SimpleECS.Core.Systems;
using ShootGame.Logic;
using ShootGame.Logic.States;
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
		bool      _isFinished;

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
					if ( !_isFinished ) {
						_isFinished = true;
						WriteResults();
					}
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
					Application.Quit();
				}
				break;
			}
		}

		void Create() {
			InputRecordConfig replayRecord = null;
			if ( ReplayShow ) {
				var replayAsset = Resources.Load<TextAsset>(ReplayPath);
				var lines = replayAsset.text.Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
				replayRecord = new InputRecordConfig(lines);
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
					_state   = State.Finish;
					_endTime = Time.realtimeSinceStartup;
					return;
				}
			}
		}

		bool UpdateOneFrame() {
			return _systems.UpdateOnce(_entities);
		}

		void WriteResults() {
			_systems.TryDispose();
			Debug.Log($"Your score is: {_entities.GetFirstComponent<ScoreState>()?.TotalScore}");
			Debug.Log($"Time: {_entities.GetFirstComponent<TimeState>().UnscaledTotalTime:0.00}");
			if ( ReplayRecord ) {
				var state = _entities.GetFirstComponent<InputRecordState>();
				state.Save($"Assets/Resources/{ReplayPath}.txt");
			}
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
