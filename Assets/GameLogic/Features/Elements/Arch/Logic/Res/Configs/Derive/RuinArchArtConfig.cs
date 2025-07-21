using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "废墟配置-Art", 
		menuName = "HungerOrHarvest/Config/Arch/废墟/Art配置", 
		order = (int) ArchType.Ruins * 3 + 1)]
	public class RuinArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.Ruins;
	}
}
