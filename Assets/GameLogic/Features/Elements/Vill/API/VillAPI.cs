using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Common.Utils;
using GameLogic.Features.Events;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	public static class VillQueryAPI {
		private static IWorld World => GameWorldMono.MainWorld;

		public static List<ulong> GetAllVillGids() {
			var query = World.CreateQueryBuilder()
				.WithAll<VillIdentityComponent>()
				.Build();
			var gids = new List<ulong>();
			query.ForEach(e => {
				gids.Add(e.GetComponent<GidComponent>().Gid);
			});
			return gids;
		}
		public static List<EntityId> GetAllVillEntityIds() {
			var query = World.CreateQueryBuilder()
				.WithAll<VillIdentityComponent>()
				.Build();
			var entityIds = new List<EntityId>();
			query.ForEach(e => {
				entityIds.Add(e.ID);
			});
			return entityIds;
		}

		public static float GetVit(Entity entity) {
			return entity.GetComponent<VillVitalityComponent>().Vit;
		}
		public static (int, float) GetJobLevelExp(Entity entity, JobType job) {
			var jobLevelComp = entity.GetComponent<JobExpComponent>();
			return (jobLevelComp.JobLevel_F[job], jobLevelComp.JobExp_F[job]);
		}

		public static ulong GetHomeArchGid(Entity vill) {
			return vill.GetComponent<BondToArchComponent>().HomeArchGid;
		}
		public static ulong GetWorkArchGid(Entity vill) {
			return vill.GetComponent<BondToArchComponent>().WorkArchGid;
		}
		public static ulong GetInArchGid(Entity vill) {
			return vill.GetComponent<InArchComponent>().ArchGid;
		}

		public static VitConfig GetVitConfig(Entity vill) {
			return GameWorldMono.MainWorld.GetResource<VillConfigResource>()
				.GetConfig(vill.GetComponent<VillIdentityComponent>().Type)
				.VitConfig;
		}

	}
	



	public static class VillRequestAPI {

		public static void RequestBondToArch(Entity vill, Entity arch) {
			vill.AddComponent(new BondToArchRequestComponent() { ArchGid = arch.GetGid(), });
		}

		public static void RequestCostVit(Entity entity, float vit, VitCostReason reason) {
			entity.AddComponent(new VillCostVitRequestComponent() {
				VitCost = vit,
				Reason = reason
			});
		}
		public static void RequestGainVit(Entity entity, float vit, VitGainReason reason) {
			entity.AddComponent(new VillGainVitRequestComponent() {
				VitGain = vit,
				Reason = reason
			});
		}

		public static void RequestGainExp(Entity entity, EtList<JobType, float> exp, ExpSource source) {
			entity.AddComponent(new ExpGainRequestComponent() {
				ExpGain = exp,
				Source = source,
			});
		}

		public static void RequestEnterWorkArch(Entity vill) {
			vill.AddComponent(new VillEnterWorkArchRequestComponent());
		}
		public static void RequestLeaveArch(Entity vill) {
			vill.AddComponent(new VillLeaveArchRequestComponent());
		}
		public static void RequestEnterHomeArch(Entity vill) {
			vill.AddComponent(new VillEnterHomeArchRequestComponent());
		}

		public static void RequestProd(Entity vill,
		EtList<RepoType, float> cons, EtList<RepoType, float> prod,
		EtList<JobType, float> expGained, float vitCost) {
			vill.AddComponent(new VillTryProdRequestComponent() {
				Cons = cons,
				Prod = prod,
				ExpGained = expGained,
				VitToCost = vitCost
			});
		}
		public static void RequestConsFoodRecoverVit(Entity vill, float foodRequest, float vitToRecover) {
			vill.AddComponent(new VillConsFoodRecoverVitRequestComponent() {
				FoodRequest = foodRequest,
				VitToRecover = vitToRecover
			});

		}

	}
}