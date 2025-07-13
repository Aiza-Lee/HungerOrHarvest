using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[CreateAssetMenu(
		fileName = "GrassLayerConfig",
		menuName = "HungerOrHarvest/Config/Layer/Grass",
		order = (int) LayerType.Grass * 2)]
	public class GrassLayerConfig : LayerConfigBase {
		public override LayerType LayerType => LayerType.Grass;

		protected override void TryAddDerivedComponents(Entity entity) {
		}
	}
}