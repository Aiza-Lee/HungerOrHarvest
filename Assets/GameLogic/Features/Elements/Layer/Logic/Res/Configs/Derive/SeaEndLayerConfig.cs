using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "SeaEndLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/SeaEnd",
		order = (int) LayerType.SeaEnd * 2)]
	public class SeaEndLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.SeaEnd;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}
