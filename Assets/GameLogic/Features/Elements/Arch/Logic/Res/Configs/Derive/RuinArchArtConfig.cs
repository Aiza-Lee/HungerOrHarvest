using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "RuinArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/RuinArt", 
		order = (int) ArchType.Ruins * 2)]
	public class RuinArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.Ruins;
	}
}
