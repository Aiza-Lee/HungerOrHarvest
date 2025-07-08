using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(fileName = "StoneMineArchConfig", menuName = "HungerOrHarvest/Config/Arch/StoneMine", order = (int)ArchType.StoneMine * 2)]
	public class StoneMineArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.StoneMine;
	}

	[CreateAssetMenu(fileName = "StoneMineArchLevelConfig", menuName = "HungerOrHarvest/Config/Arch/StoneMineLevel", order = (int)ArchType.StoneMine * 2 + 1)]
	public class StoneMineArchLevelConfig : ArchLevelConfigBase { }
}
