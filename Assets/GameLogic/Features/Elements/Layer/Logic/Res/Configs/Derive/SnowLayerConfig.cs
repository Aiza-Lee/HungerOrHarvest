using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "SnowLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/Snow",
		order = (int) LayerType.Snow * 2)]
	public class SnowLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.Snow;
	}
}
