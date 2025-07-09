using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Arch {
	/// <summary>
	/// 标识一个建筑的组件
	/// </summary>
	[System.Serializable]
	public class ArchIdentityComponent : IComponent {
		public ArchType ArchType;
	}
}