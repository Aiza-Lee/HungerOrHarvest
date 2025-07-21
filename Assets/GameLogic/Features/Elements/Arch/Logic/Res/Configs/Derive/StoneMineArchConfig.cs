using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "石矿配置", 
		menuName = "HungerOrHarvest/Config/Arch/石矿/基础配置", 
		order = (int) ArchType.StoneMine * 3)]
	public class StoneMineArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.StoneMine;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
