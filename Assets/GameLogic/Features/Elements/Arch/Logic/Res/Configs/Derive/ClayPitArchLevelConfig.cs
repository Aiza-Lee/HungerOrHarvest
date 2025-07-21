using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "粘土坑配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/粘土坑/Level配置",
		order = (int) ArchType.ClayPit * 3 + 2)]
	public class ClayPitArchLevelConfig : ArchLevelConfigBase { }
}
