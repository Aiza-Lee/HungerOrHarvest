using GameLogic.Common.DataTypes;
using GameLogic.World;

namespace GameLogic.Features.Generator {
	public static class VillGenerateAPI {
		public static void GenerateVill(VillType type, OL ol) {
			var res = GameWorldMono.MainWorld.GetResource<VillGeneratorResource>();
			res.VillDatas.Add(new VillGenerateData { Type = type, OL = ol });
		}
	}
}