using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "FarmlandArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Farmland", 
		order = (int) ArchType.Farmland * 2)]
	public class FarmlandArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Farmland;
	}

	[CreateAssetMenu(
		fileName = "FarmlandArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/FarmlandLevel", 
		order = (int) ArchType.Farmland * 2 + 1)]
	public class FarmlandArchLevelConfig : ArchLevelConfigBase { }
	
	[CreateAssetMenu(
		fileName = "FarmlandArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/FarmlandArt", 
		order = (int) ArchType.Farmland * 2)]
	public class FarmlandArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.Farmland;
	}
}
