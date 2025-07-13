using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "FarmlandArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/Farmland", 
		order = (int) ArchType.Farmland * 2)]
	public class FarmlandArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.Farmland;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
