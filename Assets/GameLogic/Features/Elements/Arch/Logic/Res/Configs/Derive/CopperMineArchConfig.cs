using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CopperMineArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/CopperMine", 
		order = (int) ArchType.CopperMine * 2)]
	public class CopperMineArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.CopperMine;
	}

	[CreateAssetMenu(
		fileName = "CopperMineArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/CopperMineLevel", 
		order = (int) ArchType.CopperMine * 2 + 1)]
	public class CopperMineArchLevelConfig : ArchLevelConfigBase { }
	
	[CreateAssetMenu(
		fileName = "CopperMineArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/CopperMineArt", 
		order = (int) ArchType.CopperMine * 2)]
	public class CopperMineArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.CopperMine;
	}
}
