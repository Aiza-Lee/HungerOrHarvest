using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "小屋配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/小屋/Level配置", 
		order = (int) ArchType.Cottage * 3 + 2)]
	public class CottageArchLevelConfig : ArchLevelConfigBase { }
}
