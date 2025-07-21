using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "耕地配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/耕地/Level配置", 
		order = (int) ArchType.Farmland * 3 + 2)]
	public class FarmlandArchLevelConfig : ArchLevelConfigBase { }
}
