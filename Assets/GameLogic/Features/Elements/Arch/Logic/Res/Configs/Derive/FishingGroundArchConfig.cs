using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "FishingGroundArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/FishingGround", 
		order = (int) ArchType.FishingGround * 2)]
	public class FishingGroundArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.FishingGround;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}
