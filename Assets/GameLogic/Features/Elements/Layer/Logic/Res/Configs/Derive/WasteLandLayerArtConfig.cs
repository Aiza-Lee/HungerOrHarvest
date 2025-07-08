using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "WasteLandLayerArtConfig",
		menuName = "HungerOrHarvest/Config/LayerArt/WasteLandArt",
		order = (int) LayerType.WasteLand * 2)]
	public class WasteLandLayerArtConfig : LayerArtConfigBase {
		public override LayerType LayerType => LayerType.WasteLand;
	}
}
