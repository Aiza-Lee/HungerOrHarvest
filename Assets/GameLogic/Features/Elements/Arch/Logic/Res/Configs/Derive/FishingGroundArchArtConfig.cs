using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "FishingGroundArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/FishingGroundArt", 
		order = (int) ArchType.FishingGround * 2)]
	public class FishingGroundArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.FishingGround;
	}
}
