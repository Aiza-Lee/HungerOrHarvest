using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表Layer 生成/销毁的Res
	/// </summary>
	[System.Serializable]
	public class LayerGeneratorResource : IResource, IWorldClearRespondable, ISaveableResource {
		public List<LayerGenerateData> LayerDatas = new();

		public void Load(IEnumerable<object> loadedData) {
			LayerDatas.Clear();
			foreach (var data in loadedData) {
				if (data is LayerGeneratorResource res) {
					LayerDatas.Clear();
					LayerDatas.AddRange(res.LayerDatas);
					break;
				}
			}
		}

		public void RespondWorldClear() {
			LayerDatas.Clear();
		}
	}

	public class LayerGenerateData {
		public LayerType Type;
		public OL OL;
		public List<IComponent> ExtraComponents = new();
	}
}