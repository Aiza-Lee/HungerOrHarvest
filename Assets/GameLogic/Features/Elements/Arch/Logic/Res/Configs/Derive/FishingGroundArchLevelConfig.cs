using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "FishingGroundArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/ArchLevel/FishingGroundLevel", 
		order = (int) ArchType.FishingDock * 2 + 1)]
	public class FishingGroundArchLevelConfig : ArchLevelConfigBase { }
}
