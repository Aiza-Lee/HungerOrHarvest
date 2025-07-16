using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch{
	[CreateAssetMenu(
		fileName = "RuinArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Ruin", 
		order = (int) ArchType.Ruins * 2)]
	public class RuinArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Ruins;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
