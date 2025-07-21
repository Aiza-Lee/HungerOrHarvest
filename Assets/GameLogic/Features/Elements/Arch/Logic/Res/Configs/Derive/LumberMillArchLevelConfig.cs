namespace GameLogic.Features.Elements.Arch {
	using GameLogic.Common.DataTypes;
	using UnityEngine;

	[CreateAssetMenu(
		fileName = "伐木场配置-Level",
		menuName = "HungerOrHarvest/Config/Arch/伐木场/Level配置",
		order = (int) ArchType.LumberMill * 3 + 2)]
	public class LumberMillArchLevelConfig : ArchLevelConfigBase { }
}
