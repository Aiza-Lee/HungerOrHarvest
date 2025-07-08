using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "TimberjackJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/TimberjackLevel",
		order = (int) JobType.Timberjack * 2 + 1)]
	public class TimberjackJobLevelConfig : JobLevelConfigBase { }
}
