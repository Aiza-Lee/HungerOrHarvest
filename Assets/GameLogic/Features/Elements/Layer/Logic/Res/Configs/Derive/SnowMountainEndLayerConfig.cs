using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "SnowMountainEndLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/SnowMountainEnd",
		order = (int) LayerType.SnowMountainEnd * 2)]
	public class SnowMountainEndLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.SnowMountainEnd;
	}
}
