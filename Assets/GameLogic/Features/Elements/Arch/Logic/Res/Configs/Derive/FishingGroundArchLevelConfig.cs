using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "渔场配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/渔场/Level配置", 
		order = (int) ArchType.FishingDock * 3 + 2)]
	public class FishingGroundArchLevelConfig : ArchLevelConfigBase { }
}
