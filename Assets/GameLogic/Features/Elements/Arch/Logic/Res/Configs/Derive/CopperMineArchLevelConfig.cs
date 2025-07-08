using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CopperMineArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/CopperMineLevel", 
		order = (int) ArchType.CopperMine * 2 + 1)]
	public class CopperMineArchLevelConfig : ArchLevelConfigBase { }
}
