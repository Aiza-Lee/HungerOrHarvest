using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "CopperMineArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/CopperMineArt", 
		order = (int) ArchType.CopperMine * 2)]
	public class CopperMineArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.CopperMine;
	}
}
