using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	public static class ArchGenerateAPI {
		public static void GenerateArch(ArchType type, OL ol, List<IComponent> extraComponents = null) {
			var res = GameWorldMono.MainWorld.GetResource<ArchGeneratorResource>();
			res.ArchDatas.Add(new ArchGenerateData {
				Type = type,
				OL = ol,
				ExtraComponents = extraComponents ?? new List<IComponent>()
			});
		}
	}
}