using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "木匠工坊配置-Level",
		menuName = "HungerOrHarvest/Config/Arch/木匠工坊/Level配置",
		order = (int) GameLogic.Common.DataTypes.ArchType.CarpentryShop * 3 + 2)]
	public class CarpenterWorkshopArchLevelConfig : ArchLevelConfigBase { }
}
