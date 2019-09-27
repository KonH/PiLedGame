using System;
using PiLedGame.Components;
using PiLedGame.State;
using PiLedGame.Systems;

namespace PiLedGame {
	public sealed class CompositionRoot {
		readonly Configuration _config;

		SystemSet _systems;

		public CompositionRoot(Configuration config) {
			_config  = config;
			_systems = new SystemSet();
		}

		public (GameState, SystemSet) Create() {
			return (CreateState(_config), CreateSystems(_config));
		}

		GameState CreateState(Configuration config) {
			var graphics = new Graphics(new Screen(8, 8));
			var debug = new Debug(updateTime: 0.15f, maxLogSize: 20);
			var random = (config.RandomSeed != 0) ? new Random(config.RandomSeed) : new Random();
			var state = new GameState(graphics, debug, random);
			GameLogic.PrepareState(state);
			return state;
		}

		SystemSet CreateSystems(Configuration config) {
			_systems = new SystemSet();
			Add(new SpeedUpSystem(10.0, 0.25));
			Add(new ResetInputSystem());
			AddIf(config.IsPlaying, () => new ReadConsoleInputSystem());
			AddIf(config.IsReplayShow, () => new FixedInputSystem(config.SavedReplayRecord));
			AddIf(config.IsReplayRecord, () => new SaveInputSystem(config.NewReplayRecord));
			Add(new ClearFrameSystem());
			Add(GameLogic.PlayerKeyboardMovement);
			Add(new InitRandomSpawnTimerSystem());
			Add(new UpdateTimerSystem());
			Add(new TimerTickSystem());
			Add(new TimerDestroySystem());
			Add(new PerformRandomSpawnSystem());
			Add(new LinearMovementSystem());
			Add(new EventMovementSystem());
			Add(new FitInsideScreenSystem());
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
			Add(new OutOfBoundsDestroySystem());
			Add(new DestroyTriggeredDamageSystem());
			Add(new DestroySystem());
			Add(new GameOverSystem());
			Add(new TrailRenderSystem());
			Add(new RenderFrameSystem());
			Add(GameLogic.HealthUI);
			Add(new FinishExecutionSystem());
			Add(new ConsoleTriggerSystem());
			Add(new ConsoleClearSystem());
			Add(new ConsoleRenderSystem());
			Add(new ConsoleLogSystem());
			AddIf(config.IsReplayShow, () => new FinishFrameFixedTimeSystem(0.0005));
			AddIf(config.IsPlaying, () => new FinishFrameRealTimeSystem());
			Add(new DeviceRenderSystem(172));
			Add(new CleanUpEventSystem());
			Add(new WaitForTargetFpsSystem(60));
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