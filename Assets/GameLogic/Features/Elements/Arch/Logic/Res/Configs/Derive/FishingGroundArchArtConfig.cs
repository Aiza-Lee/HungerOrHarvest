using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "渔场配置-Art", 
		menuName = "HungerOrHarvest/Config/Arch/渔场/Art配置", 
		order = (int) ArchType.FishingDock * 3 + 1)]
	public class FishingGroundArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.FishingDock;
	}
}
