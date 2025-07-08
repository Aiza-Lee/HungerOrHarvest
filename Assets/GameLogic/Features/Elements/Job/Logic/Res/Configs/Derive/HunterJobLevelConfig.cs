using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "HunterJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/HunterLevel",
		order = (int) JobType.Hunter * 2 + 1)]
	public class HunterJobLevelConfig : JobLevelConfigBase { }
}
