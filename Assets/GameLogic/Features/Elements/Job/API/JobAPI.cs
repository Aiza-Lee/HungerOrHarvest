using GameLogic.Common.DataTypes;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Job {
	public static class JobQueryAPI {
		private static IWorld World => GameWorldMono.MainWorld;
		private static JobConfigResource _jobConfig;
		private static JobConfigResource JobConfig => _jobConfig ??= World.GetResource<JobConfigResource>();

		public static string GetJobName(JobType jobType) {
			return JobConfig.GetConfig(jobType).JobName;
		}
		public static JobConfigBase GetJobConfig(JobType jobType) {
			return JobConfig.GetConfig(jobType);
		}
		public static JobLevelConfigBase GetJobLevelConfig(JobType jobType, int level) {
			return JobConfig.GetConfig(jobType).LevelConfigs[level];
		}

		public static float GetJobNextLevelExpDemand(JobType jobType, int level) {
			return JobConfig.GetConfig(jobType).LevelConfigs[level].NextLevelExpDemand;
		}

		public static int GetJobMaxLevel(JobType jobType) {
			return JobConfig.GetConfig(jobType).LevelConfigs.Count - 1;
		}
	}
}