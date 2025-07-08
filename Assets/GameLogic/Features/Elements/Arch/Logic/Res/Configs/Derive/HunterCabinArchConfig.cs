using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "HunterCabinArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/HunterCabin", 
		order = (int) ArchType.HunterCabin * 2)]
	public class HunterCabinArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.HunterCabin;
	}
}
