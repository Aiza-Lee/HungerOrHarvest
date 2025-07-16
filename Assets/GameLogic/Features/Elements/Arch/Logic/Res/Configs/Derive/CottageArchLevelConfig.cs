using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "CottageArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/ArchLevel/CottageLevel", 
		order = (int) ArchType.Cottage * 2 + 1)]
	public class CottageArchLevelConfig : ArchLevelConfigBase { }
}
