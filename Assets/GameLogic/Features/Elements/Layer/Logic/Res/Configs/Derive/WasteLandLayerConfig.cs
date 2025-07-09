using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "WasteLandLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/WasteLand",
		order = (int) LayerType.WasteLand * 2)]
	public class WasteLandLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.WasteLand;

		protected override void AddDerivedComponents(Entity entity) {
		}
	}
}
