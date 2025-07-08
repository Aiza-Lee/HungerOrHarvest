using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Vill {
	[CreateAssetMenu(
		fileName = "NormalVillConfig",
		menuName = "HungerOrHarvest/Config/Vill/Normal",
		order = (int) VillType.Normal * 2)]
	public class NormalVillConfig : VillConfigBase {
		public override VillType VillType => VillType.Normal;
	}

	[CreateAssetMenu(
		fileName = "NormalVillArtConfig",
		menuName = "HungerOrHarvest/Config/VillArt/NormalArt",
		order = (int) VillType.Normal * 2)]
	public class NormalVillArtConfig : VillArtConfigBase {
		public override VillType VillType => VillType.Normal;
	}
}