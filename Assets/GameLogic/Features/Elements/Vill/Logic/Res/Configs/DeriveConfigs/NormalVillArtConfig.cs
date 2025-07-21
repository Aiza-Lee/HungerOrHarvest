using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Vill {
	[CreateAssetMenu(
		fileName = "普通村民配置-Art",
		menuName = "HungerOrHarvest/Config/VillArt/普通-Art",
		order = (int) VillType.Normal * 2)]
	public class NormalVillArtConfig : VillArtConfigBase {
		public override VillType VillType => VillType.Normal;
	}
}