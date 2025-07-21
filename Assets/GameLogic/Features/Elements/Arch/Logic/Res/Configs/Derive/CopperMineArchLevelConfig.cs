using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "铜矿配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/铜矿/Level配置", 
		order = (int) ArchType.CopperMine * 3 + 2)]
	public class CopperMineArchLevelConfig : ArchLevelConfigBase { }
}
