using GameLogic.Common.DataTypes;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	/// <summary>
	/// TryProdSystem 处理"尝试生产Res"的逻辑。
	/// </summary>
	public class TryProdSystem : ISystem {
		public int Priority => 100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var dailyCnter = _world.GetResource<DailyRepoCounterResource>();
			var repoStat = _world.GetResource<RepoStatResource>();
			var tryProdInfos = _world.GetResource<TryProdInfoResource>().TryProdInfos;
			tryProdInfos.ForEach(info => {
				var cons = info.Cons;
				var prod = info.Prod;
				if (repoStat.Repos_F.BiggerThan(cons)) {
					repoStat.Repos_F.Sub(cons);
					repoStat.Repos_F.Add(prod);
					dailyCnter.DailyProdSum_F.Add(prod);
					dailyCnter.DailyConsSum_F.Add(cons);
					var villEntity = GameWorldMono.GidToEntity[info.VillGid];
					var jobExp = villEntity.GetComponent<JobExpComponent>();
					jobExp.JobExps_F.Add(info.ExpAdd);
					jobExp.IsDirty = true;
					var vitComp = villEntity.GetComponent<VillVitalityComponent>();
					vitComp.Vit -= info.VitCost;
					vitComp.IsDirty = true;
				}
			});
			tryProdInfos.Clear();
		}
		public void OnRenderUpdate(float _) { }
	} 
}