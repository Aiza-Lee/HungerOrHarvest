using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "FarmlandArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/FarmlandLevel", 
		order = (int) ArchType.Farmland * 2 + 1)]
	public class FarmlandArchLevelConfig : ArchLevelConfigBase { }
}
