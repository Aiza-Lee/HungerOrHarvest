using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表Vill 生成/销毁的Res
	/// </summary>
	[System.Serializable]
	public class VillGeneratorResource : IResource {
		public List<VillGenerateData> VillDatas = new();
	}

	public class VillGenerateData {
		public VillType Type;
		public Coord Coord;
	}
}