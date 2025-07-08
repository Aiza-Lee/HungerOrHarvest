using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {

	[CreateAssetMenu(
		fileName = "FarmerJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Farmer",
		order = (int) JobType.Farmer * 2)]
	public class FarmerJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Farmer;
	}

	[CreateAssetMenu(
		fileName = "FarmerJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/FarmerLevel",
		order = (int) JobType.Farmer * 2 + 1)]
	public class FarmerJobLevelConfig : JobLevelConfigBase { }

	[CreateAssetMenu(
		fileName = "FarmerJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/FarmerArt",
		order = (int) JobType.Farmer * 2)]
	public class FarmerJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Farmer;
	}
}