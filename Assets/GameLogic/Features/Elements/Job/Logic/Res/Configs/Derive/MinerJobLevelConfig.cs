using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "MinerJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/MinerLevel",
		order = (int) JobType.Miner * 2 + 1)]
	public class MinerJobLevelConfig : JobLevelConfigBase { }
}
