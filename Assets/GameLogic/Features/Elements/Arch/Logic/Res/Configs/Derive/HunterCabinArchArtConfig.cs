using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "HunterCabinArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/HunterCabinArt", 
		order = (int) ArchType.HunterCabin * 2)]
	public class HunterCabinArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.HunterCabin;
	}
}
