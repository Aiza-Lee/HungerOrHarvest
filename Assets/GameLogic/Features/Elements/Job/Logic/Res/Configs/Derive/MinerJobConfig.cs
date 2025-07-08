using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "MinerJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Miner",
		order = (int) JobType.Miner * 2)]
	public class MinerJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Miner;
	}

	[CreateAssetMenu(
		fileName = "MinerJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/MinerLevel",
		order = (int) JobType.Miner * 2 + 1)]
	public class MinerJobLevelConfig : JobLevelConfigBase { }

	[CreateAssetMenu(
		fileName = "MinerJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/MinerArt",
		order = (int) JobType.Miner * 2)]
	public class MinerJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Miner;
	}
}
