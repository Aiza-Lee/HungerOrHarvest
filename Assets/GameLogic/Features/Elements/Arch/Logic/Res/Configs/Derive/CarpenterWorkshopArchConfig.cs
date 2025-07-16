using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "CarpenterWorkshopArchConfig",
		menuName = "HungerOrHarvest/Config/Arch/CarpenterWorkshop",
		order = (int) ArchType.CarpentryShop * 2)]
	public class CarpenterWorkshopArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.CarpentryShop;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
