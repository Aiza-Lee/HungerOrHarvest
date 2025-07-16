namespace GameLogic.Features.Elements.Arch {
	using GameLogic.Common.DataTypes;
	using UnityEngine;

	[CreateAssetMenu(
		fileName = "LumberMillArchArtConfig",
		menuName = "HungerOrHarvest/Config/ArchArt/LumberMillArt",
		order = (int) ArchType.LumberMill * 2)]
	public class LumberMillArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.LumberMill;
	}
}