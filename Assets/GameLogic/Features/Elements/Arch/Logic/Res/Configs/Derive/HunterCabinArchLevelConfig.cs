using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "HunterCabinArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/HunterCabinLevel", 
		order = (int) ArchType.HunterCabin * 2 + 1)]
	public class HunterCabinArchLevelConfig : ArchLevelConfigBase { }
}
