using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "FisherJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/FisherLevel",
		order = (int) JobType.Fisher * 2 + 1)]
	public class FisherJobLevelConfig : JobLevelConfigBase { }
}
