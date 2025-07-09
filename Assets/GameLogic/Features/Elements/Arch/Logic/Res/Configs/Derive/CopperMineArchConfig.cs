using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CopperMineArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/CopperMine", 
		order = (int) ArchType.CopperMine * 2)]
	public class CopperMineArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.CopperMine;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}
