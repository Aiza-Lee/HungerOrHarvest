using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "FishingGroundArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/FishingGround", 
		order = (int) ArchType.FishingGround * 2)]
	public class FishingGroundArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.FishingGround;
	}

	[CreateAssetMenu(
		fileName = "FishingGroundArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/FishingGroundLevel", 
		order = (int) ArchType.FishingGround * 2 + 1)]
	public class FishingGroundArchLevelConfig : ArchLevelConfigBase { }
	
	[CreateAssetMenu(
		fileName = "FishingGroundArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/FishingGroundArt", 
		order = (int) ArchType.FishingGround * 2)]
	public class FishingGroundArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.FishingGround;
	}
}
