using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "BlacksmithJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/BlacksmithLevel",
		order = (int) JobType.Blacksmith * 2 + 1)]
	public class BlacksmithJobLevelConfig : JobLevelConfigBase { }
}
