using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "MinerJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/MinerArt",
		order = (int) JobType.Miner * 2)]
	public class MinerJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Miner;
	}
}
