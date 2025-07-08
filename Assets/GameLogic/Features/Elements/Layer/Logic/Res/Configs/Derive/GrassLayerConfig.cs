using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "GrassLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/Grass",
		order = (int) LayerType.Grass * 2)]
	public class GrassLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.Grass;
	}
}