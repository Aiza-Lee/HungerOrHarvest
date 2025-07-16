using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "HunterCabinArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/HunterCabin", 
		order = (int) ArchType.HuntingCabin * 2)]
	public class HunterCabinArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.HuntingCabin;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
