using GameLogic.Common.DataTypes;
using GameLogic.World;

namespace GameLogic.Features.Generator {
	public static class LayerGenerateAPI {
		public static void GenerateLayer(LayerType type, OL ol) {
			var res = GameWorldMono.MainWorld.GetResource<LayerGeneratorResource>();
			res.LayerDatas.Add(new LayerGenerateData { Type = type, OL = ol });
		}
	}
}