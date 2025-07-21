namespace GameLogic.Features.Elements.Arch {
	using GameLogic.Common.DataTypes;
	using NsEcsFrame.Core;
	using UnityEngine;

	[CreateAssetMenu(
		fileName = "伐木场配置",
		menuName = "HungerOrHarvest/Config/Arch/伐木场/基础配置",
		order = (int) ArchType.LumberMill * 3)]
	public class LumberMillArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.LumberMill;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}