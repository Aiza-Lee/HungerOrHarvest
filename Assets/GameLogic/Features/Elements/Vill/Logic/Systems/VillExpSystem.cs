using GameLogic.Features.Job;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// VillExpSystem 处理村民的工作经验和等级提升逻辑。负责处理jobExpComponent中添加过的工作经验，并根据经验值更新JobExpComponent中的工作等级。
	/// </summary>
	public class VillExpSystem : ISystem {
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
			var entities = _world.CreateQueryBuilder().WithAll<VillIdentityComponent>().Build();
			var jobConfigRes = _world.GetResource<JobConfigResource>();
			entities.ForEach(e => {
				var expComp = e.GetComponent<JobExpComponent>();
				if (!expComp.IsDirty) return;
				expComp.JobLevels_F.ForEach(jobLevel => {
					var jobConfig = jobConfigRes.GetConfig(jobLevel.EnumType);
					var levelUpDemand = jobConfig.LevelConfigs[jobLevel.Value].NextLevelExpDemand;
					if (expComp.JobExps_F[jobLevel.EnumType] >= levelUpDemand) {
						if (jobLevel.Value < jobConfig.LevelConfigs.Count - 1) {
							expComp.JobExps_F[jobLevel.EnumType] -= levelUpDemand;
							jobLevel.Value++;
						} else {
							expComp.JobExps_F[jobLevel.EnumType] = levelUpDemand;
						}
					}
				});
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}