using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "HunterCabinArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/ArchLevel/HunterCabinLevel", 
		order = (int) ArchType.HuntingCabin * 2 + 1)]
	public class HunterCabinArchLevelConfig : ArchLevelConfigBase { }
}
