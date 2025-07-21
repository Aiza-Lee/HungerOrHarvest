using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "石矿配置-Art", 
		menuName = "HungerOrHarvest/Config/Arch/石矿/Art配置", 
		order = (int) ArchType.StoneMine * 3 + 1)]
	public class StoneMineArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.StoneMine;
	}
}
