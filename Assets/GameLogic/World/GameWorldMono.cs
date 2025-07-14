using System.Collections.Generic;
using GameLogic.Common.View;
using GameLogic.Features.AutoSortingLayer;
using GameLogic.Features.Destroyer;
using GameLogic.Features.Elements.Vill;
using GameLogic.Features.Generator;
using GameLogic.Features.Job;
using GameLogic.Features.Layer;
using GameLogic.Features.MainCamera;
using GameLogic.Features.WorldDataManager;
using GameLogic.Features.Repo;
using GameLogic.Features.SpeedControl;
using GameLogic.Features.TickCounter;
using GameLogic.Features.TickSpeed;
using GameLogic.Features.Vill;
using GameLogic.Features.WorldEdge;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using GameLogic.Features.UiData.StartMenuData;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Events;

namespace GameLogic.World {
	public class GameWorldMono : WorldBehaviour {
		public static Dictionary<ulong, Entity> GidToEntity = new();

		protected override void RegisterSystems() {
			World.SystemManager
				/* Common */
				.RegisterSystem<CoordToTransformSystem>()
				.RegisterSystem<OlToTransformSystem>()
				.RegisterSystem<CoordToSmoothPositionSystem>()
				.RegisterSystem<SmoothChangeSystem>()

				/* Event */
				.RegisterSystem<LogicFrameRequestConsumeSystem>()
				.RegisterSystem<LogicFrameRequestConversionSystem>()

				/* AutoSoringOrder */
				.RegisterSystem<AutoSortingLayerSystem>()

				/* Tick */
				.RegisterSystem<TickSpeedSystem>()
				.RegisterSystem<TickCounterSystem>()

				/* SpeedControl */
				.RegisterSystem<SpeedControlInputSystem>()
				.RegisterSystem<SpeedControlSystem>()

				/* WorldDataManager */
				.RegisterSystem<LoadGameCmdSystem>()
				.RegisterSystem<DayEndAutoSaveSystem>()
				.RegisterSystem<NewWorldCreatorSystem>()

				/* Repo */
				.RegisterSystem<DayFirstTickClearCounterSystem>()
				.RegisterSystem<TryProdSystem>()

				/* MainCamera */
				.RegisterSystem<CameraInputSystem>()
				.RegisterSystem<CameraMoveSystem>()

				/* Generator */
				.RegisterSystem<ArchGeneratorSystem>()
				.RegisterSystem<LayerGeneratorSystem>()
				.RegisterSystem<VillGeneratorSystem>()

				/* Arch */
				.RegisterSystem<BondToArchSystem>()

				/* Vill */
				.RegisterSystem<VillAiSystem>()
				.RegisterSystem<VillEnterLeaveArchSystem>()
				.RegisterSystem<VitalitySystem>()
				.RegisterSystem<ExpSystem>()
				.RegisterSystem<BondToVillSystem>()

				/* Destroy */
				.RegisterSystem<ArchDestroyerSystem>()
				.RegisterSystem<VillDestroyerSystem>()

				/* WorldEdge */
				.RegisterSystem<WorldEdgeSystem>()

				/* Sync */
				.RegisterSystem<CameraSyncSystem>()
				.RegisterSystem<RectTransformSyncSystem>()
				.RegisterSystem<SpriteRendererSyncSystem>()
				.RegisterSystem<TransformSyncSystem>()

				/* EventConsumer */
				.RegisterSystem<GeneratedEventConsumerSystem_Logic>()
				.RegisterSystem<GeneratedEventConsumerSystem_View>()
				.RegisterSystem<DestroyedEventConsumerSystem_Logic>()
				.RegisterSystem<DestroyedEventConsumerSystem_View>()
				.RegisterSystem<SaveEventConsumerSystem_Logic>()

			/* UI */
				/* StartMenu */
				.RegisterSystem<StartMenuDataSystem>()
				
			;
		}
		protected override void RegisterResources() {
			World
				/* Generator */
				.InsertResource(new VillGeneratorResource())
				.InsertResource(new LayerGeneratorResource())
				.InsertResource(new ArchGeneratorResource())

				/* Tick */
				.InsertResource(new TickSpeedResource())
				.InsertResource(new TickCounterResource())
				.InsertResource(new TickConfigResource())

				/* SpeedControlInput */
				.InsertResource(new SpeedControlInputResource())
				.InsertResource(new SpeedControlConfigResource())

				/* Repo */
				.InsertResource(new RepoStatResource())
				.InsertResource(new TryProdInfoResource())
				.InsertResource(new DailyRepoCounterResource())

				/* MainCamera */
				.InsertResource(new CameraInputResource())
				.InsertResource(new CameraConfigResource())

				/* Element */
				.InsertResource(new VillConfigResource())
				.InsertResource(new ArchConfigResource())
				.InsertResource(new UnlockedArchResource())
				.InsertResource(new LayerConfigResource())
				.InsertResource(new JobConfigResource())

				/* Destroy */
				.InsertResource(new VillDestroyResource())
				.InsertResource(new ArchDestroyResource())

				/* SaveLoadGame */
				.InsertResource(new SaveInfoResource())
				.InsertResource(new LoadGameCmdResource())

				/* NewWorldCreator */
				.InsertResource(new RandomWorldConfigResource())
				.InsertResource(new NewWorldInfoResource())

				/* WorldEdge */
				.InsertResource(new WorldEdgeResource())

			/* UI */
				/* StartMenu */
				.InsertResource(new StartMenuDataResource())
			;
		}
	}
}