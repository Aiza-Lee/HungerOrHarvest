using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "BlacksmithJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Blacksmith",
		order = (int) JobType.Blacksmith * 2)]
	public class BlacksmithJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Blacksmith;
	}

	[CreateAssetMenu(
		fileName = "BlacksmithJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/BlacksmithLevel",
		order = (int) JobType.Blacksmith * 2 + 1)]
	public class BlacksmithJobLevelConfig : JobLevelConfigBase { }

	[CreateAssetMenu(
		fileName = "BlacksmithJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/BlacksmithArt",
		order = (int) JobType.Blacksmith * 2)]
	public class BlacksmithJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Blacksmith;
	}
}
