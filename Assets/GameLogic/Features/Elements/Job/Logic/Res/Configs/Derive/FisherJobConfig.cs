using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "FisherJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Fisher",
		order = (int) JobType.Fisher * 2)]
	public class FisherJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Fisher;
	}

	[CreateAssetMenu(
		fileName = "FisherJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/FisherLevel",
		order = (int) JobType.Fisher * 2 + 1)]
	public class FisherJobLevelConfig : JobLevelConfigBase { }

	[CreateAssetMenu(
		fileName = "FisherJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/FisherArt",
		order = (int) JobType.Fisher * 2)]
	public class FisherJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Fisher;
	}
}
