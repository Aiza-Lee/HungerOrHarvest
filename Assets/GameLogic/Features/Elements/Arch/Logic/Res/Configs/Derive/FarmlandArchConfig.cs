using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "耕地配置", 
		menuName = "HungerOrHarvest/Config/Arch/耕地/基础配置", 
		order = (int) ArchType.Farmland * 3)]
	public class FarmlandArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Farmland;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
