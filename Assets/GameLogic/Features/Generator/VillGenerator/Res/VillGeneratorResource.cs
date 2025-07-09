using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表Vill 生成/销毁的Res
	/// </summary>
	[System.Serializable]
	public class VillGeneratorResource : IResource {
		public List<VillGenerateInfo> VillGenerateInfos = new();
	}

	[System.Serializable]
	public class VillGenerateInfo {
		public VillVitalityComponent VillVitalityState;
		public VillIdentityComponent VillIdentity;
		public Coord Coord;
		public JobExpComponent VillJobExp;
	}
}