using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "黏土坑配置-Art",
		menuName = "HungerOrHarvest/Config/Arch/黏土坑/Art配置",
		order = (int) ArchType.ClayPit * 3 + 1)]
	public class ClayPitArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.ClayPit;
	}
}
