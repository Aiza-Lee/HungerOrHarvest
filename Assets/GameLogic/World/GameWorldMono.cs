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
				.RegisterSystem<TickSpeedSystem>()
				.RegisterSystem<TickCounterSystem>()
				.RegisterSystem<LoadGameCmdSystem>()
				.RegisterSystem<DayEndAutoSaveSystem>()
				.RegisterSystem<DayFirstTickClearCounterSystem>()
				.RegisterSystem<TryProdSystem>()
				.RegisterSystem<CameraInputSystem>()
				.RegisterSystem<ArchGeneratorSystem>()
				.RegisterSystem<LayerGeneratorSystem>()
				.RegisterSystem<VillGeneratorSystem>()
				.RegisterSystem<VillExpSystem>()
				.RegisterSystem<VillAiSystem>()
				.RegisterSystem<VillSpriteSoringOrderSystem>()
				.RegisterSystem<ArchDestroyerSystem>()
				.RegisterSystem<VillDestroyerSystem>()
				.RegisterSystem<CoordToTransformSystem>()
				.RegisterSystem<SmoothChangeSystem>()
				.RegisterSystem<SmoothedCoordToSmoothChangeStatSystem>()
				.RegisterSystem<CameraSyncSystem>()
				.RegisterSystem<CameraMoveSystem>()
				.RegisterSystem<RectTransformSyncSystem>()
				.RegisterSystem<SpriteRendererSyncSystem>()
				.RegisterSystem<TransformSyncSystem>()
				.RegisterSystem<OLToCoordSystem>()
			;
		}
		protected override void RegisterResources() {
			World
				.InsertResource(new ChangeCurveResource())
				.InsertResource(new VillGeneratorResource())
				.InsertResource(new LayerGeneratorResource())
				.InsertResource(new ArchGeneratorResource())
				.InsertResource(new TickSpeedResource())
				.InsertResource(new TickCounterResource())
				.InsertResource(new TickConfigResource())
				.InsertResource(new RepoStatResource())
				.InsertResource(new TryProdInfoResource())
				.InsertResource(new DailyRepoCounterResource())
				.InsertResource(new CameraInputResource())
				.InsertResource(new CameraConfigResource())
				.InsertResource(new VillConfigResource())
				.InsertResource(new ArchConfigResource())
				.InsertResource(new LayerConfigResource())
				.InsertResource(new JobConfigResource())
				.InsertResource(new VillDestroyResource())
				.InsertResource(new ArchDestroyResource())
				.InsertResource(new LoadGameCmdResource())
			;
		}

		public void ClearWorld() {

			WorldClearRegistry.Inst.RespondWorldClear();

			var systems = World.SystemManager.GetAllSystems();
			foreach (var system in systems) {
				if (system is IWorldClearRespondable respondable) {
					respondable.RespondWorldClear();
				}
			}

			var reses = World.GetAllResources();
			foreach (var res in reses) {
				if (res is IWorldClearRespondable respondable) {
					respondable.RespondWorldClear();
				}
			}

			var entities = World.GetAllEntities();
			foreach (var entity in entities) {
				if (!entity.HasComponent<IgnoreWorldClearComponent>()) {
					if (entity.HasComponent<GidComponent>()) {
						var gid = entity.GetComponent<GidComponent>().Gid;
						if (GidToEntity.ContainsKey(gid)) {
							GidToEntity.Remove(gid);
						}
					}
					World.DestroyEntity(entity.ID);
				}
			}
		}

	}
}