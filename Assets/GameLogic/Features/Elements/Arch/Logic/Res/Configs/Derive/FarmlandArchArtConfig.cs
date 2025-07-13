using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "FarmlandArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/FarmlandArt", 
		order = (int) ArchType.Farmland * 2)]
	public class FarmlandArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.Farmland;
	}
}
