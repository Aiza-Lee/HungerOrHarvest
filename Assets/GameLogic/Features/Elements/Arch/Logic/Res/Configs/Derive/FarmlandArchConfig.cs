using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "FarmlandArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Farmland", 
		order = (int) ArchType.Farmland * 2)]
	public class FarmlandArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Farmland;
	}
}
