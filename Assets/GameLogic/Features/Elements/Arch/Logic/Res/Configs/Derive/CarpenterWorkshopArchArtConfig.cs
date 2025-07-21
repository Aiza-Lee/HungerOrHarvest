using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "木匠工坊配置-Art",
		menuName = "HungerOrHarvest/Config/Arch/木匠工坊/Art配置",
		order = (int) ArchType.CarpentryShop * 3 + 1)]
	public class CarpenterWorkshopArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.CarpentryShop;
	}
}
