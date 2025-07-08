using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "BeachLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/Beach",
		order = (int) LayerType.Beach * 2)]
	public class BeachLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.Beach;
	}
}
