using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CarpenterWorkshopArchArtConfig",
		menuName = "HungerOrHarvest/Config/ArchArt/CarpenterWorkshopArt",
		order = (int) ArchType.CarpenterWorkshop * 2)]
	public class CarpenterWorkshopArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.CarpenterWorkshop;
	}
}
