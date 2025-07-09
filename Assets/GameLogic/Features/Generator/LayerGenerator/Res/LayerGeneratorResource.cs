using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.Layer;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表Layer 生成/销毁的Res
	/// </summary>
	[System.Serializable]
	public class LayerGeneratorResource : IResource {
		public List<LayerGenerateInfo> LayerGenerateInfos = new();
	}
	
	[System.Serializable]
	public class LayerGenerateInfo {
		public LayerIdentityComponent LayerIdentity;
		public Coord Coord;
	}
}