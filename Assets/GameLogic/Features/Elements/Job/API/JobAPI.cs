using GameLogic.Common.DataTypes;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Job {
	public static class JobQueryAPI {
		private static IWorld World => GameWorldMono.MainWorld;

		public static string GetJobName(JobType jobType) {
			return World.GetResource<JobConfigResource>().GetConfig(jobType).JobName;
		}
		public static JobConfigBase GetJobConfig(JobType jobType) {
			return World.GetResource<JobConfigResource>().GetConfig(jobType);
		}
		public static JobLevelConfigBase GetJobLevelConfig(JobType jobType, int level) {
			return World.GetResource<JobConfigResource>().GetConfig(jobType).LevelConfigs[level];
		}
	}
}