using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "FisherJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/FisherArt",
		order = (int) JobType.Fisher * 2)]
	public class FisherJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Fisher;
	}
}
