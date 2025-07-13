using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.WorldDataManager;
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
				if (data is ArchGeneratorResource res) {
					ArchDatas.Clear();
					ArchDatas.AddRange(res.ArchDatas);
					break;
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
		public List<IComponent> ExtraComponents = new();
	}
}