using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "MinerJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Miner",
		order = (int) JobType.Miner * 2)]
	public class MinerJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Miner;
	}
}
