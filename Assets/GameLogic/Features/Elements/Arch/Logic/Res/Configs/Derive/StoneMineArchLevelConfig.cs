using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "StoneMineArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/StoneMineLevel", 
		order = (int) ArchType.StoneMine * 2 + 1)]
	public class StoneMineArchLevelConfig : ArchLevelConfigBase { }
}
