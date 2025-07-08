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
}
