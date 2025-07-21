using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "耕地配置-Art", 
		menuName = "HungerOrHarvest/Config/Arch/耕地/Art配置", 
		order = (int) ArchType.Farmland * 3 + 1)]
	public class FarmlandArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.Farmland;
	}
}
