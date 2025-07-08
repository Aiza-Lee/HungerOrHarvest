using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "FarmerJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/FarmerLevel",
		order = (int) JobType.Farmer * 2 + 1)]
	public class FarmerJobLevelConfig : JobLevelConfigBase { }
}
