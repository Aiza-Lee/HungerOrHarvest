using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "RuinArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Ruin", 
		order = (int) ArchType.Ruin * 2)]
	public class RuinArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Ruin;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}
