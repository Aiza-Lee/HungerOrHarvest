using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "RuinArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/ArchLevel/RuinLevel", 
		order = (int) ArchType.Ruins * 2 + 1)]
	public class RuinArchLevelConfig : ArchLevelConfigBase { }
}
