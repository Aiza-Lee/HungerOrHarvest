using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "HunterJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Hunter",
		order = (int) JobType.Hunter * 2)]
	public class HunterJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Hunter;
	}

	[CreateAssetMenu(
		fileName = "HunterJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/HunterLevel",
		order = (int) JobType.Hunter * 2 + 1)]
	public class HunterJobLevelConfig : JobLevelConfigBase { }

	[CreateAssetMenu(
		fileName = "HunterJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/HunterArt",
		order = (int) JobType.Hunter * 2)]
	public class HunterJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Hunter;
	}
}
