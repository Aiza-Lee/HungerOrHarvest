using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CarpenterWorkshopArchLevelConfig",
		menuName = "HungerOrHarvest/Config/Arch/CarpenterWorkshopLevel",
		order = (int) GameLogic.Common.DataTypes.ArchType.CarpenterWorkshop * 2 + 1)]
	public class CarpenterWorkshopArchLevelConfig : ArchLevelConfigBase { }
}
