using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "BlacksmithJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Blacksmith",
		order = (int) JobType.Blacksmith * 2)]
	public class BlacksmithJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Blacksmith;
	}
}
