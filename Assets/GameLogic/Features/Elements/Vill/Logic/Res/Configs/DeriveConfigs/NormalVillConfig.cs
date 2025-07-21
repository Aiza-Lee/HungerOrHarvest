using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Vill {
	[CreateAssetMenu(
		fileName = "普通村民配置",
		menuName = "HungerOrHarvest/Config/Vill/普通",
		order = (int) VillType.Normal * 2)]
	public class NormalVillConfig : VillConfigBase {
		public override VillType VillType => VillType.Normal;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}