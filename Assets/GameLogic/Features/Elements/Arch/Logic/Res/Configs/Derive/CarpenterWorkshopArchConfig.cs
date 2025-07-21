using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "木匠工坊配置",
		menuName = "HungerOrHarvest/Config/Arch/木匠工坊/基础配置",
		order = (int) ArchType.CarpentryShop * 3)]
	public class CarpenterWorkshopArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.CarpentryShop;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
