using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "FisherJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Fisher",
		order = (int) JobType.Fisher * 2)]
	public class FisherJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Fisher;
	}
}
