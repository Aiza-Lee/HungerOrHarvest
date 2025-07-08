using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CottageArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Cottage", 
		order = (int) ArchType.Cottage * 2)]
	public class CottageArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Cottage;
	}
}