using GameLogic.Features.Events;
using GameLogic.Features.Job;
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

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_queryBuilder = _world.CreateQueryBuilder().WithAll<ExpGainRequestComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_queryBuilder.Build().ForEach(vill => {
				var request = vill.GetComponent<ExpGainRequestComponent>();
				var expComp = vill.GetComponent<JobExpComponent>();
				request.ExpGain.ForEach(pr => {
					var type = pr.EnumType;
					var level = expComp.JobLevel_F[type];
					expComp.JobExp_F[pr.EnumType] += pr.Value;

					var nextLevelExpDemand = JobQueryAPI.GetJobNextLevelExpDemand(type, level);
					var maxLevel = JobQueryAPI.GetJobMaxLevel(type);

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