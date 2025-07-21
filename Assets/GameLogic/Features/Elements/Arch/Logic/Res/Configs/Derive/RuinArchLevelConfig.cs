using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "废墟配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/废墟/Level配置", 
		order = (int) ArchType.Ruins * 3 + 2)]
	public class RuinArchLevelConfig : ArchLevelConfigBase { }
}
