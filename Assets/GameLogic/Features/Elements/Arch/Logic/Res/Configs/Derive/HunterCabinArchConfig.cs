using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[CreateAssetMenu(
		fileName = "猎庐配置", 
		menuName = "HungerOrHarvest/Config/Arch/猎庐/基础配置", 
		order = (int) ArchType.HuntingCabin * 3)]
	public class HunterCabinArchConfig : ArchConfigBase {
		public override ArchType ArchType => ArchType.HuntingCabin;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}
