using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "铜矿配置-Art", 
		menuName = "HungerOrHarvest/Config/Arch/铜矿/Art配置", 
		order = (int) ArchType.CopperMine * 3 + 1)]
	public class CopperMineArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.CopperMine;
	}
}
