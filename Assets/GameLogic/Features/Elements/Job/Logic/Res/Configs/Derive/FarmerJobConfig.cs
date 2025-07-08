using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "FarmerJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Farmer",
		order = (int) JobType.Farmer * 2)]
	public class FarmerJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Farmer;
	}
}