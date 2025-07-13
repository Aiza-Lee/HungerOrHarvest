using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "ClayPitArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/ClayPitArt", 
		order = (int) ArchType.ClayPit * 2)]
	public class ClayPitArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.ClayPit;
	}
}
