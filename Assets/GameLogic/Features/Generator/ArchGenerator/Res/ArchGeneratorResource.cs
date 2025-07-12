using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表建筑生成的Resource
	/// </summary>
	[System.Serializable]
	public class ArchGeneratorResource : IResource, IWorldClearRespondable, ISaveableResource {
		public List<ArchGenerateData> ArchDatas = new();

		public void Load(IEnumerable<object> loadedData) {
			// 清空现有数据
			ArchDatas.Clear();

			foreach (var data in loadedData) {
				if (data is ArchGenerateData archData) {
					ArchDatas.Add(archData);
				}
			}
		}

		public void RespondWorldClear() {
			ArchDatas.Clear();
		}
	}

	public class ArchGenerateData {
		public ArchType Type;
		public OL OL;
	}
}