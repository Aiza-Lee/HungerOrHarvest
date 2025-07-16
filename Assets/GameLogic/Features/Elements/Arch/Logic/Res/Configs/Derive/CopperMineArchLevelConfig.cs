using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "CopperMineArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/ArchLevel/CopperMineLevel", 
		order = (int) ArchType.CopperMine * 2 + 1)]
	public class CopperMineArchLevelConfig : ArchLevelConfigBase { }
}
