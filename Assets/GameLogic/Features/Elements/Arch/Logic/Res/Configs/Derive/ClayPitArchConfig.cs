using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[CreateAssetMenu(
		fileName = "ClayPitArchConfig", 
		menuName = "HungerOrHarvest/Config/Arch/ClayPit", 
		order = (int) ArchType.ClayPit * 2)]
	public class ClayPitArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.ClayPit;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}
