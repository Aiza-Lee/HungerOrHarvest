using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "TimberjackJobConfig",
		menuName = "HungerOrHarvest/Config/Job/Timberjack",
		order = (int) JobType.Timberjack * 2)]
	public class TimberjackJobConfig : JobConfigBase {
		public override JobType JobType => JobType.Timberjack;
	}
}
