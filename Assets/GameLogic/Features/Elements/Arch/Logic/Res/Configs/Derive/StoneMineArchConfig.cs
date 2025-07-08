using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "StoneMineArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/StoneMine", 
		order = (int) ArchType.StoneMine * 2)]
	public class StoneMineArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.StoneMine;
	}
}
