namespace GameLogic.Features.Elements.Arch {
	using GameLogic.Common.DataTypes;
	using UnityEngine;

	[CreateAssetMenu(
		fileName = "伐木场配置-Art",
		menuName = "HungerOrHarvest/Config/Arch/伐木场/Art配置",
		order = (int) ArchType.LumberMill * 3 + 1)]
	public class LumberMillArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.LumberMill;
	}
}