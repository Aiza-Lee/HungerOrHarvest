using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表Layer 生成/销毁的Res
	/// </summary>
	[System.Serializable]
	public class LayerGeneratorResource : IResource, IWorldClearRespondable {
		public List<LayerGenerateData> LayerDatas = new();

		public void RespondWorldClear() {
			LayerDatas.Clear();
		}
	}
	
	public class LayerGenerateData {
		public LayerType Type;
		public OL OL;
	}
}