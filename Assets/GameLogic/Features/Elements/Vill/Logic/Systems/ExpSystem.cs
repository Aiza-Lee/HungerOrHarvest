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
				expGainRequest.ExpGain.ForEach(pr => {
					var jobType = pr.EnumType;
					var level = expComp.JobLevel_F[jobType];
					expComp.JobExp_F[pr.EnumType] += pr.Value;

					var nextLevelExpDemand = JobQueryAPI.GetJobNextLevelExpDemand(jobType, level);
					var maxLevel = JobQueryAPI.GetJobMaxLevel(jobType);

					if (level >= maxLevel) {
						pr.Value = Mathf.Min(pr.Value, nextLevelExpDemand);
						return;
					}
					
					if (expComp.JobExp_F[pr.EnumType] >= nextLevelExpDemand) {
						expComp.JobExp_F[pr.EnumType] -= nextLevelExpDemand;
						expComp.JobLevel_F[pr.EnumType] += 1;
						vill.AddComponent(new VillJobLevelUpEventComponent() { JobType = pr.EnumType });
					}
				});
			});
		}
		public void OnRenderUpdate(float _) { }
	}
}