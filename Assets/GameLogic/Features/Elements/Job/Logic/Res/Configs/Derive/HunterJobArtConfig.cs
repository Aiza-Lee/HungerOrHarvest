using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "HunterJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/HunterArt",
		order = (int) JobType.Hunter * 2)]
	public class HunterJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Hunter;
	}
}
