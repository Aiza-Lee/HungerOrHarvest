using GameLogic.Common.DataTypes;
using GameLogic.World;

namespace GameLogic.Features.Generator {
	public static class ArchGenerateAPI {
		public static void GenerateArch(ArchType type, OL ol) {
			var res = GameWorldMono.MainWorld.GetResource<ArchGeneratorResource>();
			res.ArchDatas.Add(new ArchGenerateData { Type = type, OL = ol });
		}
	}
}