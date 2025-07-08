using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "BeachLayerArtConfig",
		menuName = "HungerOrHarvest/Config/LayerArt/BeachArt",
		order = (int) LayerType.Beach * 2)]
	public class BeachLayerArtConfig : LayerArtConfigBase {
		public override LayerType LayerType => LayerType.Beach;
	}
}
