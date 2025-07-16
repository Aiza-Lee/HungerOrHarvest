using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "CarpenterWorkshopArchLevelConfig",
		menuName = "HungerOrHarvest/Config/ArchLevel/CarpenterWorkshopLevel",
		order = (int) GameLogic.Common.DataTypes.ArchType.CarpentryShop * 2 + 1)]
	public class CarpenterWorkshopArchLevelConfig : ArchLevelConfigBase { }
}
