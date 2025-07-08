using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "FarmerJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/FarmerArt",
		order = (int) JobType.Farmer * 2)]
	public class FarmerJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Farmer;
	}
}
