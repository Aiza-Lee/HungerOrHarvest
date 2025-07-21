using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch{
	[CreateAssetMenu(
		fileName = "废墟配置",
		menuName = "HungerOrHarvest/Config/Arch/废墟/基础配置",
		order = (int) ArchType.Ruins * 3)]
	public class RuinArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Ruins;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
