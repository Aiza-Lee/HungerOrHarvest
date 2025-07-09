using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "CottageArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Cottage", 
		order = (int) ArchType.Cottage * 2)]
	public class CottageArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Cottage;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}