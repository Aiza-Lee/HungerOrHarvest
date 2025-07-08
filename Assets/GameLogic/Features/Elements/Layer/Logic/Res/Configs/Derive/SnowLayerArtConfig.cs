using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "SnowLayerArtConfig",
		menuName = "HungerOrHarvest/Config/LayerArt/SnowArt",
		order = (int) LayerType.Snow * 2)]
	public class SnowLayerArtConfig : LayerArtConfigBase {
		public override LayerType LayerType => LayerType.Snow;
	}
}
