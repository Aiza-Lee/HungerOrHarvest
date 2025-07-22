using GameLogic.Common.DataTypes;
using GameLogic.Features.Elements.Vill;
using GameLogic.Features.Events;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Repo {
	/// <summary>
	/// TryProdSystem 处理"尝试生产Res"的逻辑。
	/// </summary>
	public class TryProdSystem : ISystem {
		public int Priority => 1000;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _villTryProdQuery, _archTryProdQuery, _villRecoverQuery;

		private DailyRepoCounterResource _dailyCnter;
		private DailyRepoCounterResource DailyCnter => _dailyCnter ??= _world.GetResource<DailyRepoCounterResource>();

		private RepoStatResource _repoStat;
		private RepoStatResource RepoStat => _repoStat ??= _world.GetResource<RepoStatResource>();

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

			// 建筑的产出请求
			_archTryProdQuery.Build().ForEach(arch => {
				var tryProd = arch.GetComponent<ArchTryProdRequestComponent>();
				CalculateConsProd(tryProd.Cons, tryProd.Prod);
			});

			// 村民的产出请求
			_villTryProdQuery.Build().ForEach(vill => {
				var tryProd = vill.GetComponent<VillTryProdRequestComponent>();
				if (CalculateConsProd(tryProd.Cons, tryProd.Prod)) {
					VillRequestAPI.RequestGainExp(vill, tryProd.ExpGained, ExpSource.Production);
					VillRequestAPI.RequestCostVit(vill, tryProd.VitToCost, VitCostReason.Production);
				}
			});

			// 村民消耗食物，恢复体力的请求
			_villRecoverQuery.Build().ForEach(vill => {
				var recover = vill.GetComponent<VillConsFoodRecoverVitRequestComponent>();
				if (RepoStat.Repos_F[RepoType.Food] >= recover.FoodRequest) {
					RepoStat.Repos_F[RepoType.Food] -= recover.FoodRequest;
					DailyCnter.DailyConsSum_F[RepoType.Food] += recover.FoodRequest;
					VillRequestAPI.RequestGainVit(vill, recover.VitToRecover, VitGainReason.EatFood);
				}
			});
		}
		public void OnRenderUpdate(float _) { }

		private bool CalculateConsProd(EtList<RepoType, float> cons, EtList<RepoType, float> prod) {
			if (RepoStat.Repos_F.BiggerThan(cons)) {
				RepoStat.Repos_F.Sub(cons);
				DailyCnter.DailyConsSum_F.Add(cons);
				prod.ForEach(pr => {
					var limit = RepoStat.RepoMax_F[pr.EnumType] - RepoStat.Repos_F[pr.EnumType];
					var add = Mathf.Min(limit, pr.Value);
					RepoStat.Repos_F[pr.EnumType] += add;
					DailyCnter.DailyProdSum_F[pr.EnumType] += add;
				});
				return true;
			}
			return false;
		}
	} 
}