using System;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Components;
using ShootGame.Logic.Systems;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace ShootGame.Logic {
	public sealed class CompositionRoot {
		readonly ScreenConfig  _screen;
		readonly DebugConfig   _debug;
		readonly Configuration _config;

		readonly Func<BaseReadInputSystem>            _inputSystem;
		readonly Func<ISystem>[]                      _preRenderSystems;
		readonly Func<BaseFinishFrameRealTimeSystem>  _realTimeSystem;
		readonly Func<BaseRenderSystem>               _renderSystem;

		SystemSet _systems;


		public CompositionRoot(
			ScreenConfig screen,
			DebugConfig debug,
			Configuration config,
			Func<BaseReadInputSystem> inputSystem,
			Func<ISystem>[] preRenderSystems,
			Func<BaseFinishFrameRealTimeSystem> realTimeSystem,
			Func<BaseRenderSystem> renderSystem) {
			_screen = screen;
			_debug  = debug;
			_config = config;

			_inputSystem      = inputSystem;
			_preRenderSystems = preRenderSystems;
			_realTimeSystem   = realTimeSystem;
			_renderSystem     = renderSystem;

			_systems = new SystemSet();
		}

		public (EntitySet, SystemSet) Create() {
			var entities = CreateState(_screen);
			return (entities, CreateSystems(_config, _debug, _screen, entities));
		}

		EntitySet CreateState(ScreenConfig screen) {
			var entities = new EntitySet();
			GameLogic.PrepareState(screen, entities);
			return entities;
		}

		SystemSet CreateSystems(Configuration config, DebugConfig debug, ScreenConfig screen, EntitySet entities) {
			entities.Add().AddComponent(new ExecutionState());
			entities.Add().AddComponent(new InputState());
			entities.Add().AddComponent(new TimeState());
			entities.Add().AddComponent(new FrameState(screen));
			if ( debug != null ) {
				entities.Add().AddComponent(new DebugState(debug));
			}
			entities.Add().AddComponent(new RandomState((_config.RandomSeed != 0) ? new RandomConfig(_config.RandomSeed) : new RandomConfig()));
			_systems = new SystemSet();
			Add(new SpeedUpSystem(new SpeedUpConfig(10.0, 0.25)));
			Add(new ResetInputSystem());
			AddIf(config.IsPlaying, _inputSystem);
			AddIf(config.IsReplayShow, () => new FixedInputSystem(config.SavedReplayRecord));
			AddIf(config.IsReplayRecord, () => {
				entities.Add().AddComponent(new InputRecordState());
				return new SaveInputSystem();
			});
			Add(new ClearFrameSystem());
			Add(GameLogic.PlayerKeyboardMovement);
			Add(new InitRandomSpawnTimerSystem());
			Add(new UpdateTimerSystem());
			Add(new TimerTickSystem());
			Add(new TimerDestroySystem());
			Add(new PerformRandomSpawnSystem());
			Add(new LinearMovementSystem());
			Add(new EventMovementSystem());
			Add(new FitInsideScreenSystem(screen));
			Add(new CollisionSystem());
			Add(GameLogic.PreventSpawnCollisions);
			Add(GameLogic.PreventHealthSpawnIfNotRequired);
			Add(GameLogic.SpawnItems);
			Add(GameLogic.SpawnObstacles);
			Add(GameLogic.PlayerShoots);
			Add(GameLogic.BonusBulletSpawn);
			Add(new SendDamageSystem());
			Add(new ApplyDamageSystem());
			Add(new CollectItemSystem());
			Add(new AddToInventorySystem());
			Add(GameLogic.Healing);
			Add(new AddHealthSystem());
			Add(new ScoreMeasureSystem());
			Add(new DestroyCollectedItemSystem());
			Add(new NoHealthDestroySystem());
			Add(new OutOfBoundsDestroySystem(screen));
			Add(new DestroyTriggeredDamageSystem());
			Add(new DestroySystem());
			Add(new GameOverSystem());
			Add(new TrailRenderSystem());
			Add(new RenderFrameSystem());
			Add(GameLogic.HealthUI);
			Add(new FinishExecutionSystem());
			foreach ( var system in _preRenderSystems ) {
				Add(system());
			}
			AddIf(config.IsReplayShow, () => new FinishFrameFixedTimeSystem(new FinishFrameFixedTimeConfig(_config.FixedFrameTime)));
			AddIf(config.IsPlaying, _realTimeSystem);
			Add(_renderSystem());
			Add(new CleanUpEventSystem());
			return _systems;
		}

		void Add(ISystem system) {
			_systems.Add(system);
		}

		void Add(ISystem[] systems) {
			foreach ( var system in systems ) {
				Add(system);
			}
		}

		void AddIf(bool condition, Func<ISystem> systemCtor) {
			if ( condition ) {
				Add(systemCtor());
			}
		}
	}
}