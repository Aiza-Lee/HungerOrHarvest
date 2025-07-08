using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "SnowMountainEndLayerArtConfig",
		menuName = "HungerOrHarvest/Config/LayerArt/SnowMountainEndArt",
		order = (int) LayerType.SnowMountainEnd * 2)]
	public class SnowMountainEndLayerArtConfig : LayerArtConfigBase {
		public override LayerType LayerType => LayerType.SnowMountainEnd;
	}
}
