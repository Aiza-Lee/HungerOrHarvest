using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.ClearWorld;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表建筑生成的Resource
	/// </summary>
	[System.Serializable]
	public class ArchGeneratorResource : IResource, IWorldClearRespondable {
		public List<ArchGenerateData> ArchDatas = new();

		public void RespondWorldClear() {
			ArchDatas.Clear();
		}
	}

	public class ArchGenerateData {
		public ArchType Type;
		public OL OL;
	}
}