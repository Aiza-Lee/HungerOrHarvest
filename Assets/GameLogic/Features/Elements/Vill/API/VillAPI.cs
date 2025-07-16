using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Common.Utils;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Events;
using GameLogic.Features.Job;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Features.Elements.Vill {
	public static class VillQueryAPI {
		private static IWorld World => GameWorldMono.MainWorld;
		private static readonly EntityQueryBuilder _villQueryBuilder = World.CreateQueryBuilder()
			.WithAll<VillIdentityComponent>();

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

		public static float GetVit(Entity entity) => entity.GetComponent<VillVitalityComponent>().Vit;
		public static float GetVitPercentage(Entity entity) {
			var type = entity.GetComponent<VillIdentityComponent>().Type;
			var vitConfig = GetVitConfig(type);
			return GetVit(entity) / vitConfig.MaxVit;
		}
		public static (int, float) GetJobLevelExp(Entity entity, JobType job) {
			var jobLevelComp = entity.GetComponent<JobExpComponent>();
			return (jobLevelComp.JobLevel_F[job], jobLevelComp.JobExp_F[job]);
		}
		public static string GetName(Entity vill) {
			var identityComp = vill.GetComponent<VillIdentityComponent>();
			return $"{identityComp.LastName}{identityComp.FirstName}";
		}
		public static VillType GetVillType(Entity vill) => vill.GetComponent<VillIdentityComponent>().Type;

		public static ulong GetHomeArchGid(Entity vill) => vill.GetComponent<BondToArchComponent>().HomeArchGid;
		public static ulong GetWorkArchGid(Entity vill) => vill.GetComponent<BondToArchComponent>().WorkArchGid;
		public static ulong GetInArchGid(Entity vill) => vill.GetComponent<InArchComponent>().ArchGid;
		public static VillArtConfigBase GetArtConfig(VillType type) => World.GetResource<VillConfigResource>().GetArtConfig(type);
		public static VitConfig GetVitConfig(VillType villType) {
			return World.GetResource<VillConfigResource>()
				.GetConfig(villType).VitConfig;
		}
		public static IEnumerable<EtPair<JobType, int>> GetJobLevels(Entity vill) => vill.GetComponent<JobExpComponent>().JobLevel_F;
		public static List<(JobType, int, float)> GetSortedJobLevels(Entity vill) {
			var jobLevelComp = vill.GetComponent<JobExpComponent>();
			var list = new List<EtPair<JobType, int>>(jobLevelComp.JobLevel_F);
			list.Sort((a, b) => b.Value.CompareTo(a.Value));
			var res = new List<(JobType, int, float)>();
			foreach (var jobLevel in list) {
				var jobType = jobLevel.EnumType;
				var level = jobLevel.Value;
				var expProportion = GetJobExpProportion(vill, jobType);
				res.Add((jobType, level, expProportion));
			}
			return res;
		}
		public static float GetJobExpProportion(Entity vill, JobType job) {
			var jobLevelComp = vill.GetComponent<JobExpComponent>();
			var lConfig = JobQueryAPI.GetJobLevelConfig(job, jobLevelComp.JobLevel_F[job]);
			return jobLevelComp.JobExp_F[job] / lConfig.NextLevelExpDemand;
		}
		public static List<Entity> GetNoHomeVills() {
			var res = new List<Entity>();
			_villQueryBuilder.Build().ForEach(vill => {
				if (vill.GetComponent<BondToArchComponent>().HomeArchGid == 0) { res.Add(vill); }
			});
			return res;
		}
		public static List<Entity> GetHaveHomeNoWorkVills() {
			var res = new List<Entity>();
			_villQueryBuilder.Build().ForEach(vill => {
				var bondComp = vill.GetComponent<BondToArchComponent>();
				if (bondComp.WorkArchGid == 0 && bondComp.HomeArchGid != 0) { res.Add(vill); }
			});
			return res;
		}

	}

	public static class VillDirectOperationAPI {
		public static void BondToArch(Entity vill, Entity arch) {
			var bondComp = vill.GetComponent<BondToArchComponent>();
			var archType = ArchQueryAPI.GetType(arch);
			if (archType == ArchType.Cottage) {
				bondComp.HomeArchGid = arch.GetGid();
			} else {
				bondComp.WorkArchGid = arch.GetGid();
			}
		}

		public static void DisbondArch(Entity vill, Entity arch) {
			var bondComp = vill.GetComponent<BondToArchComponent>();
			if (bondComp.WorkArchGid == arch.GetGid()) {
				bondComp.WorkArchGid = 0;
			} else if (bondComp.HomeArchGid == arch.GetGid()) {
				bondComp.HomeArchGid = 0;
			} else {
				throw new System.Exception("Cannot disbond from arch, vill is not bonded to this arch.");
			}
		}

		public static void EnterWorkArch(Entity vill) {
			var bondComp = vill.GetComponent<BondToArchComponent>();
			if (bondComp.WorkArchGid == 0) {
				throw new System.Exception("Cannot enter work arch, vill is not bonded to a work arch.");
			}
			vill.GetComponent<InArchComponent>().ArchGid = bondComp.WorkArchGid;
		}
		public static void EnterHomeArch(Entity vill) {
			var bondComp = vill.GetComponent<BondToArchComponent>();
			if (bondComp.HomeArchGid == 0) {
				throw new System.Exception("Cannot enter home arch, vill is not bonded to a home arch.");
			}
			vill.GetComponent<InArchComponent>().ArchGid = bondComp.HomeArchGid;
		}
		public static void LeaveArch(Entity vill) {
			var inArchComp = vill.GetComponent<InArchComponent>();
			if (inArchComp.ArchGid == 0) {
				throw new System.Exception("Cannot leave arch, vill is not in any arch.");
			}
			inArchComp.ArchGid = 0;
		}
	}

	public static class VillRequestAPI {

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