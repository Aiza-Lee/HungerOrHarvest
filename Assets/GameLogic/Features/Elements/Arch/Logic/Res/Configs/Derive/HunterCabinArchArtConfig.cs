using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "猎庐配置-Art", 
		menuName = "HungerOrHarvest/Config/Arch/猎庐/Art配置", 
		order = (int) ArchType.HuntingCabin * 3 + 1)]
	public class HunterCabinArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.HuntingCabin;
	}
}
