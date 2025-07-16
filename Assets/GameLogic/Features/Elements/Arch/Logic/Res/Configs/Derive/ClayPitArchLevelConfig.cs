using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "ClayPitArchLevelConfig", 
		menuName = "HungerOrHarvest/Config/ArchLevel/ClayPitLevel", 
		order = (int) ArchType.ClayPit * 2 + 1)]
	public class ClayPitArchLevelConfig : ArchLevelConfigBase { }
}
