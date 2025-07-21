using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "渔场配置", 
		menuName = "HungerOrHarvest/Config/Arch/渔场/基础配置", 
		order = (int) ArchType.FishingDock * 3)]
	public class FishingGroundArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.FishingDock;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
