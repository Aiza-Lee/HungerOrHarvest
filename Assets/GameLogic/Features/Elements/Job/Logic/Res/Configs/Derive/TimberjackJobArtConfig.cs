using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Job {
	[CreateAssetMenu(
		fileName = "TimberjackJobArtConfig",
		menuName = "HungerOrHarvest/Config/JobArt/TimberjackArt",
		order = (int) JobType.Timberjack * 2)]
	public class TimberjackJobArtConfig : JobArtConfigBase {
		public override JobType JobType => JobType.Timberjack;
	}
}
