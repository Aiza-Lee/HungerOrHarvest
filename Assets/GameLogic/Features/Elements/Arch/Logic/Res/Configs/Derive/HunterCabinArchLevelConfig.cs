using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "猎庐配置-Level", 
		menuName = "HungerOrHarvest/Config/Arch/猎庐/Level配置", 
		order = (int) ArchType.HuntingCabin * 3 + 2)]
	public class HunterCabinArchLevelConfig : ArchLevelConfigBase { }
}
