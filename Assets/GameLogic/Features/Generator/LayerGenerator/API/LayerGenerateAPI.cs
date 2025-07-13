using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	public static class LayerGenerateAPI {
		public static void GenerateLayer(LayerType type, OL ol, List<IComponent> extraComponents = null) {
			var res = GameWorldMono.MainWorld.GetResource<LayerGeneratorResource>();
			res.LayerDatas.Add(new LayerGenerateData {
				Type = type,
				OL = ol,
				ExtraComponents = extraComponents ?? new List<IComponent>()});
		}
	}
}