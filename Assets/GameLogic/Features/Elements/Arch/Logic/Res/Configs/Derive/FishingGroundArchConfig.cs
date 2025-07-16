using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "FishingGroundArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/FishingGround", 
		order = (int) ArchType.FishingDock * 2)]
	public class FishingGroundArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.FishingDock;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
