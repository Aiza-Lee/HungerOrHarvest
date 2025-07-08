using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(fileName = "CarpenterWorkshopArchConfig", menuName = "HungerOrHarvest/Config/Arch/CarpenterWorkshop", order = (int)ArchType.CarpenterWorkshop * 2)]
	public class CarpenterWorkshopArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.CarpenterWorkshop;
	}

	[CreateAssetMenu(fileName = "CarpenterWorkshopArchLevelConfig", menuName = "HungerOrHarvest/Config/Arch/CarpenterWorkshopLevel", order = (int)ArchType.CarpenterWorkshop * 2 + 1)]
	public class CarpenterWorkshopArchLevelConfig : ArchLevelConfigBase { }
}
