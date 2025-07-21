using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "石矿配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/石矿/Level配置", 
		order = (int) ArchType.StoneMine * 3 + 2)]
	public class StoneMineArchLevelConfig : ArchLevelConfigBase { }
}
