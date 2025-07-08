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

	[CreateAssetMenu(
		fileName = "TimberjackJobLevelConfig",
		menuName = "HungerOrHarvest/Config/Job/TimberjackLevel",
		order = (int) JobType.Timberjack * 2 + 1)]
	public class TimberjackJobLevelConfig : JobLevelConfigBase { }

	[CreateAssetMenu(
		fileName = "TimberjackJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/TimberjackArt",
		order = (int) JobType.Timberjack * 2)]
	public class TimberjackJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Timberjack;
	}
}
