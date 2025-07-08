using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "ClayPitArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/Arch/ClayPitLevel", 
		order = (int) ArchType.ClayPit * 2 + 1)]
	public class ClayPitArchLevelConfig : ArchLevelConfigBase { }
}
