using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CottageArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Cottage", 
		order = (int) ArchType.Cottage * 2)]
	public class CottageArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Cottage;
	}

	[CreateAssetMenu(
		fileName = "CottageArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/CottageLevel", 
		order = (int) ArchType.Cottage * 2 + 1)]
	public class CottageArchLevelConfig : ArchLevelConfigBase { }
	
	[CreateAssetMenu(
		fileName = "CottageArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/CottageArt", 
		order = (int) ArchType.Cottage * 2)]
	public class CottageArchArtConfig : ArchArtConfigBase {	
		public override ArchType ArchType => ArchType.Cottage;
	}
}