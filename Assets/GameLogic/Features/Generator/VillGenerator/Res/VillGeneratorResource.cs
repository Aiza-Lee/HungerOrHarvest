using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;

namespace GameLogic.Features.VillGenerator {
	[System.Serializable]
	public class VillGeneratorResource : IResource {
		public List<VillGenerateInfo> VillGenerateInfos = new();
	}

	[System.Serializable]
	public class VillGenerateInfo {
		public VillStatComponent VillStat;
		public VillIdentityComponent VillIdentity;
		public Coord Coord;
		public JobExpComponent VillJobExp;
	}
}