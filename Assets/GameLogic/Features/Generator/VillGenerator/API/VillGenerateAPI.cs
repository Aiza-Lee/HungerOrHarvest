using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	public static class VillGenerateAPI {
		public static void Generate(VillType type, Coord coord, List<IComponent> extraComponents = null) {
			var res = GameWorldMono.MainWorld.GetResource<VillGeneratorResource>();
			res.VillDatas.Add(new VillGenerateData {
				Type = type,
				Coord = coord,
				ExtraComponents = extraComponents ?? new List<IComponent>()
			});
		}
	}
}