using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.Arch;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 代表建筑生成/销毁的Resource
	/// </summary>
	[System.Serializable]
	public class ArchGeneratorResource : IResource {
		public List<ArchGenerateInfo> ArchGenerateInfos = new();
	}


	[System.Serializable]
	public class ArchGenerateInfo {
		public ArchIdentityComponent ArchIdentity;
		public Coord Coord;
	}
}