using GameLogic.Common.DataTypes;
using GameLogic.Features.Elements.Vill;
using GameLogic.Features.Events;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	/// <summary>
	/// TryProdSystem 处理"尝试生产Res"的逻辑。
	/// </summary>
	public class TryProdSystem : ISystem {
		public int Priority => 1000;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _villTryProdQuery, _archTryProdQuery, _villRecoverQuery;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_villTryProdQuery = _world.CreateQueryBuilder().WithAll<VillTryProdRequestComponent>();
			_archTryProdQuery = _world.CreateQueryBuilder().WithAll<ArchTryProdRequestComponent>();
			_villRecoverQuery = _world.CreateQueryBuilder().WithAll<VillConsFoodRecoverVitRequestComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var dailyCnter = _world.GetResource<DailyRepoCounterResource>();
			var repoStat = _world.GetResource<RepoStatResource>();

			_archTryProdQuery.Build().ForEach(arch => {
				var tryProd = arch.GetComponent<ArchTryProdRequestComponent>();
				if (repoStat.Repos_F.BiggerThan(tryProd.Cons)) {
					repoStat.Repos_F.Sub(tryProd.Cons);
					repoStat.Repos_F.Add(tryProd.Prod);
					dailyCnter.DailyConsSum_F.Add(tryProd.Cons);
					dailyCnter.DailyProdSum_F.Add(tryProd.Prod);
				}
			});

			_villTryProdQuery.Build().ForEach(vill => {
				var tryProd = vill.GetComponent<VillTryProdRequestComponent>();
				if (repoStat.Repos_F.BiggerThan(tryProd.Cons)) {
					repoStat.Repos_F.Sub(tryProd.Cons);
					repoStat.Repos_F.Add(tryProd.Prod);
					dailyCnter.DailyConsSum_F.Add(tryProd.Cons);
					dailyCnter.DailyProdSum_F.Add(tryProd.Prod);
					VillRequestAPI.RequestGainExp(vill, tryProd.ExpGained, ExpSource.Production);
					VillRequestAPI.RequestCostVit(vill, tryProd.VitToCost, VitCostReason.Production);
				}
			});

			_villRecoverQuery.Build().ForEach(vill => {
				var recover = vill.GetComponent<VillConsFoodRecoverVitRequestComponent>();
				if (repoStat.Repos_F[RepoType.Food] >= recover.FoodRequest) {
					repoStat.Repos_F[RepoType.Food] -= recover.FoodRequest;
					dailyCnter.DailyConsSum_F[RepoType.Food] += recover.FoodRequest;
					VillRequestAPI.RequestGainVit(vill, recover.VitToRecover, VitGainReason.EatFood);
				}
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}