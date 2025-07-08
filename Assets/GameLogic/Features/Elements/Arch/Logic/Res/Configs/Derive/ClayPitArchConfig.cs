using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(fileName = "ClayPitArchConfig", menuName = "HungerOrHarvest/Config/Arch/ClayPit", order = (int)ArchType.ClayPit * 2)]
	public class ClayPitArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.ClayPit;
	}

	[CreateAssetMenu(fileName = "ClayPitArchLevelConfig", menuName = "HungerOrHarvest/Config/Arch/ClayPitLevel", order = (int)ArchType.ClayPit * 2 + 1)]
	public class ClayPitArchLevelConfig : ArchLevelConfigBase { }
}
