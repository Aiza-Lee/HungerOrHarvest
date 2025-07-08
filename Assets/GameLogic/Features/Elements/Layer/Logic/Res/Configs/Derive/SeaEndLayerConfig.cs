using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "SeaEndLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/SeaEnd",
		order = (int) LayerType.SeaEnd * 2)]
	public class SeaEndLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.SeaEnd;
	}

	[CreateAssetMenu(
		fileName = "SeaEndLayerArtConfig",
		menuName = "HungerOrHarvest/Config/LayerArt/SeaEndArt",
		order = (int) LayerType.SeaEnd * 2)]
	public class SeaEndLayerArtConfig : LayerArtConfigBase {
		public override LayerType LayerType => LayerType.SeaEnd;
	}
}
