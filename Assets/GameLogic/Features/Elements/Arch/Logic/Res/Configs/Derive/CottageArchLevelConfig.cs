using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CottageArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/CottageLevel", 
		order = (int) ArchType.Cottage * 2 + 1)]
	public class CottageArchLevelConfig : ArchLevelConfigBase { }
}
