using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表Vill 生成/销毁的Res
	/// </summary>
	[System.Serializable]
	public class VillGeneratorResource : IResource, IWorldClearRespondable {
		public List<VillGenerateData> VillDatas = new();

		public void RespondWorldClear() {
			VillDatas.Clear();
		}
	}

	public class VillGenerateData {
		public VillType Type;
		public OL OL;
	}
}