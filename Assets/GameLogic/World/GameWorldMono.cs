using System.Collections.Generic;
using GameLogic.Common.Logic;
using GameLogic.Common.View;
using GameLogic.Features.Arch;
using GameLogic.Features.ClearWorld;
using GameLogic.Features.Destroyer;
using GameLogic.Features.Elements.Vill;
using GameLogic.Features.Generator;
using GameLogic.Features.Job;
using GameLogic.Features.Layer;
using GameLogic.Features.MainCamera;
using GameLogic.Features.NewWorldCreator;
using GameLogic.Features.Repo;
using GameLogic.Features.SaveLoadData;
using GameLogic.Features.TickCounter;
using GameLogic.Features.TickSpeed;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.World {
	public class GameWorldMono : WorldBehaviour {
		public static Dictionary<ulong, Entity> GidToEntity = new();

		protected override void RegisterSystems() {
			World.SystemManager
				/* Tick */
				.RegisterSystem<TickSpeedSystem>()
				.RegisterSystem<TickCounterSystem>()

				/* SaveLoadGame */
				.RegisterSystem<LoadGameCmdSystem>()
				.RegisterSystem<DayEndAutoSaveSystem>()

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

				/* Vill */
				.RegisterSystem<VillExpSystem>()
				.RegisterSystem<VillAiSystem>()
				.RegisterSystem<VillSpriteSoringOrderSystem>()

				/* Destroy */
				.RegisterSystem<ArchDestroyerSystem>()
				.RegisterSystem<VillDestroyerSystem>()

				/* Common */
				.RegisterSystem<SmoothedCoordToSmoothChangeStatSystem>()
				.RegisterSystem<SmoothChangeSystem>()
				.RegisterSystem<CoordToTransformSystem>()
				.RegisterSystem<OLToCoordSystem>()

				/* Sync */
				.RegisterSystem<CameraSyncSystem>()
				.RegisterSystem<RectTransformSyncSystem>()
				.RegisterSystem<SpriteRendererSyncSystem>()
				.RegisterSystem<TransformSyncSystem>()
			;
		}
		protected override void RegisterResources() {
			World
				/* Common */
				.InsertResource(new ChangeCurveResource())

				/* Generator */
				.InsertResource(new VillGeneratorResource())
				.InsertResource(new LayerGeneratorResource())
				.InsertResource(new ArchGeneratorResource())

				/* Tick */
				.InsertResource(new TickSpeedResource())
				.InsertResource(new TickCounterResource())
				.InsertResource(new TickConfigResource())

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
				.InsertResource(new LayerConfigResource())
				.InsertResource(new JobConfigResource())

				/* Destroy */
				.InsertResource(new VillDestroyResource())
				.InsertResource(new ArchDestroyResource())

				/* SaveLoadGame */
				.InsertResource(new LoadGameCmdResource())

				/* NewWorldCreator */
				.InsertResource(new RandomWorldConfigResource())
				.InsertResource(new NewWorldInfoResource())
			;
		}
	}
}