using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "RuinArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/RuinLevel", 
		order = (int) ArchType.Ruin * 2 + 1)]
	public class RuinArchLevelConfig : ArchLevelConfigBase { }
}
