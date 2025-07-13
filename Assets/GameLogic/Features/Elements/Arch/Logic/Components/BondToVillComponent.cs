using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	/// <summary>
	/// 记录建筑与村民之间的绑定关系的组件
	/// </summary>
	public class BondToVillComponent : IComponent {
		public List<ulong> BondedVillGids = new();
		public bool IsDirty;
	}
}