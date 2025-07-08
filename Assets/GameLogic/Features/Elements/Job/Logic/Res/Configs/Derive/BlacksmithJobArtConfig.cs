using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "BlacksmithJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/BlacksmithArt",
		order = (int) JobType.Blacksmith * 2)]
	public class BlacksmithJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Blacksmith;
	}
}
