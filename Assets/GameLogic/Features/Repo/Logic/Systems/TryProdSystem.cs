using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	/// <summary>
	/// TryProdSystem 处理尝试生产的逻辑。
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
			var tryProds = _world.GetResource<TryProdInfoResource>();
			tryProds.TryProdInfos.ForEach(info => {
				var cons = info.Cons;
				var prod = info.Prod;
				if (repoStat.Repos_F.BiggerThan(cons)) {
					repoStat.Repos_F.Sub(cons);
					repoStat.Repos_F.Add(prod);
					dailyCnter.DailyProdSum_F.Add(prod);
					dailyCnter.DailyConsSum_F.Add(cons);
					info.Succeed = true;
				}
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}