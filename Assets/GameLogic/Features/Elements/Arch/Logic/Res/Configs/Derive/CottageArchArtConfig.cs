using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "小屋配置-Art", 
		menuName = "HungerOrHarvest/Config/Arch/小屋/Art配置", 
		order = (int) ArchType.Cottage * 3 + 1)]
	public class CottageArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.Cottage;
	}
}
