using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "粘土坑配置",
		menuName = "HungerOrHarvest/Config/Arch/粘土坑/基础配置",
		order = (int) ArchType.ClayPit * 3)]
	public class ClayPitArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.ClayPit;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
