using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "FishingGroundArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/FishingGroundLevel", 
		order = (int) ArchType.FishingGround * 2 + 1)]
	public class FishingGroundArchLevelConfig : ArchLevelConfigBase { }
}
