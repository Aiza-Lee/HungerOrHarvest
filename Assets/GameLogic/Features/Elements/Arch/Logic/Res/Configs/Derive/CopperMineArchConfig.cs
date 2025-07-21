using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "铜矿配置", 
		menuName = "HungerOrHarvest/Config/Arch/铜矿/基础配置", 
		order = (int) ArchType.CopperMine * 3)]
	public class CopperMineArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.CopperMine;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
