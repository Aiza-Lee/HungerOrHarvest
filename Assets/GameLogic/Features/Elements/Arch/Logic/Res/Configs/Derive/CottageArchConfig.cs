using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "小屋配置", 
		menuName = "HungerOrHarvest/Config/Arch/小屋/基础配置", 
		order = (int) ArchType.Cottage * 3)]
	public class CottageArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Cottage;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}