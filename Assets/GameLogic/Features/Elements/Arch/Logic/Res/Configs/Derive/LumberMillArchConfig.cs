namespace GameLogic.Features.Elements.Arch {
	using GameLogic.Common.DataTypes;
	using NsEcsFrame.Core;
	using UnityEngine;

	[CreateAssetMenu(
		fileName = "LumberMillArchConfig",
		menuName = "HungerOrHarvest/Config/Arch/LumberMill",
		order = (int) ArchType.LumberMill * 2)]
	public class LumberMillArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.LumberMill;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}