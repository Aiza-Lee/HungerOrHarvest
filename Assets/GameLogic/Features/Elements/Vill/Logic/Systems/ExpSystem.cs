using GameLogic.Common.DataTypes;
using GameLogic.Features.Events;
using GameLogic.Features.Job;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// ExpSystem 负责处理经验增加请求
	/// </summary>
	public class ExpSystem : ISystem {
		public int Priority => 1500;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _queryBuilder;
		private JobConfigResource _jobConfig;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_queryBuilder = _world.CreateQueryBuilder().WithAll<ExpGainRequestComponent>();
			_jobConfig = _world.GetResource<JobConfigResource>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_queryBuilder.WithAll<VillIdentityComponent>().Build().ForEach(vill => {
				var expGainRequest = vill.GetComponent<ExpGainRequestComponent>();
				var expComp = vill.GetComponent<JobExpComponent>();
				expComp.JobExp_F.Add(expGainRequest.ExpGain);

				expComp.JobExp_F.ForEach(pr => {
					var level = expComp.JobLevel_F[pr.EnumType];
					var config = _jobConfig.GetConfig(pr.EnumType);
					var lConfig = config.LevelConfigs[level];
					if (level >= config.LevelConfigs.Count - 1) {
						pr.Value = Mathf.Min(pr.Value, lConfig.NextLevelExpDemand);
						return;
					}
					if (pr.Value >= lConfig.NextLevelExpDemand) {
						pr.Value -= lConfig.NextLevelExpDemand;
						expComp.JobLevel_F[pr.EnumType] += 1;
						vill.AddComponent(new VillJobLevelUpEventComponent() { JobType = pr.EnumType });
					}
				});

			});
		}
		public void OnRenderUpdate(float _) { }
	}
}