using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "CottageArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/CottageArt", 
		order = (int) ArchType.Cottage * 2)]
	public class CottageArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.Cottage;
	}
}
