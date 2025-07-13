using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "StoneMineArchArtConfig", 
		menuName = "HungerOrHarvest/Config/ArchArt/StoneMineArt", 
		order = (int) ArchType.StoneMine * 2)]
	public class StoneMineArchArtConfig : ArchArtConfigBase {
		public override ArchType ArchType => ArchType.StoneMine;
	}
}
