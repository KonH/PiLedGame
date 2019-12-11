using System;
using ShootGame.Logic.Events;
using ShootGame.Logic.Systems;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Events;
using SimpleECS.Core.States;
using SimpleECS.Core.Systems;
using SimpleECS.Core.Utils.Caching;

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

		(Cache<Entity> entityCache, CacheScope componentCache, CacheScope getCache) FillCaches() {
			var entityCache = new Cache<Entity>(128);
			var componentCache = new CacheScope()
				.Init<PositionComponent>(256)
				.Init<SpawnComponent>(64)
				.Init<TimerComponent>(32)
				.Init<TimerTickEvent>()
				.Init<SpawnEvent>(32)
				.Init<MovementEvent>(8)
				.Init<CollisionEvent>(32)
				.Init<SendDamageEvent>(32)
				.Init<ApplyDamageEvent>(32)
				.Init<AddItemEvent>()
				.Init<CollectItemEvent>()
				.Init<SpawnBonusBulletEvent>(16)
				.Init<DestroyEvent>(32)
				.Init<AddHealthEvent>()
				.Init<RandomSpawnComponent>(32)
				.Init<SolidBodyComponent>(32)
				.Init<RenderComponent>(32)
				.Init<PlayerComponent>(1)
				.Init<KeyboardMovementComponent>(1)
				.Init<InventoryComponent>(1)
				.Init<KeyboardSpawnComponent>()
				.Init<HealthComponent>(16)
				.Init<FitInsideScreenComponent>(16)
				.Init<LinearMovementComponent>(32)
				.Init<OutOfBoundsDestroyComponent>(32)
				.Init<DamageComponent>(16)
				.Init<TrailComponent>(16);
			var getCache = new CacheScope()
				.Init<ComponentCollection<InputState>.Enumerator>()
				.Init<ComponentCollection<FrameState>.Enumerator>()
				.Init<EntityComponentCollection<SpawnComponent>.Enumerator>()
				.Init<EntityComponentCollection<KeyboardMovementComponent>.Enumerator>()
				.Init<EntityComponentCollection<RandomSpawnComponent>.Enumerator>()
				.Init<EntityComponentCollection<TimerComponent>.Enumerator>()
				.Init<EntityComponentCollection<TimerComponent, TimerTickEvent>.Enumerator>()
				.Init<EntityComponentCollection<SpawnComponent, RandomSpawnComponent, TimerTickEvent>.Enumerator>()
				.Init<EntityComponentCollection<LinearMovementComponent>.Enumerator>()
				.Init<EntityComponentCollection<PositionComponent, MovementEvent>.Enumerator>()
				.Init<ComponentCollection<FitInsideScreenComponent, PositionComponent>.Enumerator>()
				.Init<EntityComponentCollection<PositionComponent, SolidBodyComponent>.Enumerator>(32)
				.Init<EntityComponentCollection<SpawnEvent, CollisionEvent>.Enumerator>()
				.Init<ComponentCollection<PlayerComponent, HealthComponent>.Enumerator>()
				.Init<EntityComponentCollection<SpawnEvent>.Enumerator>()
				.Init<ComponentCollection<PositionComponent, SpawnEvent>.Enumerator>(8)
				.Init<EntityComponentCollection<SpawnComponent, KeyboardSpawnComponent>.Enumerator>()
				.Init<EntityComponentCollection<InventoryComponent>.Enumerator>()
				.Init<EntityComponentCollection<SpawnBonusBulletEvent>.Enumerator>()
				.Init<EntityComponentCollection<DamageComponent, CollisionEvent>.Enumerator>()
				.Init<ComponentCollection<ApplyDamageEvent, HealthComponent>.Enumerator>()
				.Init<EntityComponentCollection<InventoryComponent, CollisionEvent>.Enumerator>()
				.Init<ComponentCollection<InventoryComponent, AddItemEvent>.Enumerator>()
				.Init<ComponentCollection<HealthComponent, AddHealthEvent>.Enumerator>()
				.Init<EntityComponentCollection<HealthComponent, DestroyEvent>.Enumerator>()
				.Init<EntityComponentCollection<AddItemEvent>.Enumerator>()
				.Init<EntityComponentCollection<CollectItemEvent>.Enumerator>()
				.Init<EntityComponentCollection<HealthComponent>.Enumerator>()
				.Init<EntityComponentCollection<OutOfBoundsDestroyComponent, PositionComponent>.Enumerator>()
				.Init<EntityComponentCollection<DamageComponent, SendDamageEvent>.Enumerator>()
				.Init<EntityComponentCollection<DestroyEvent>.Enumerator>()
				.Init<EntityComponentCollection<PlayerComponent>.Enumerator>()
				.Init<EntityComponentCollection<TrailComponent, PositionComponent>.Enumerator>()
				.Init<ComponentCollection<PositionComponent, RenderComponent>.Enumerator>()
				.Init<EntityComponentCollection<InputState>.Enumerator>()
				.Init<ComponentCollection<TimeState>.Enumerator>()
				.Init<EntityCollection.Enumerator>()
				.Init<ComponentCollection<ExecutionState>.Enumerator>();
			return (entityCache, componentCache, getCache);
		}

		EntitySet CreateState(ScreenConfig screen) {
			var (entityCache, componentCache, getCache) = FillCaches();
			var entities = new EntitySet(entityCache, componentCache, getCache);
			GameLogic.PrepareState(screen, entities);
			return entities;
		}

		SystemSet CreateSystems(Configuration config, DebugConfig debug, ScreenConfig screen, EntitySet entities) {
			entities.Add().AddComponent(new ExecutionState());
			entities.Add().AddComponent(new InputState());
			entities.Add().AddComponent(new TimeState());
			entities.Add().AddComponent(new FrameState().Init(screen));
			if ( debug != null ) {
				entities.Add().AddComponent(new DebugState().Init(debug));
			}
			entities.Add().AddComponent(new RandomState().Init((_config.RandomSeed != 0) ? new RandomConfig(_config.RandomSeed) : new RandomConfig()));
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