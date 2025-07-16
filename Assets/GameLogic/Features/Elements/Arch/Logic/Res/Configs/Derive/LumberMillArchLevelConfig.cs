namespace GameLogic.Features.Elements.Arch {
	using GameLogic.Common.DataTypes;
	using UnityEngine;

	[CreateAssetMenu(
		fileName = "LumberMillArchLevelConfig",
		menuName = "HungerOrHarvest/Config/ArchLevel/LumberMill",
		order = (int) ArchType.LumberMill * 2 + 1)]
	public class LumberMillArchLevelConfig : ArchLevelConfigBase { }
}
