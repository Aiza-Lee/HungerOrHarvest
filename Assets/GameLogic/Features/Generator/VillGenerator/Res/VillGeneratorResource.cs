using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表Vill 生成/销毁的Res
	/// </summary>
	[System.Serializable]
	public class VillGeneratorResource : IResource, IWorldClearRespondable, ISaveableResource {
		public List<VillGenerateData> VillDatas = new();

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is VillGeneratorResource res) {
					VillDatas.Clear();
					VillDatas.AddRange(res.VillDatas);
					break;
				}
			}
		}

		public void RespondWorldClear() {
			VillDatas.Clear();
		}
	}

	public class VillGenerateData {
		public VillType Type;
		public OL OL;
	}
}