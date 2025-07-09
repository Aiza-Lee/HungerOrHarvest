using System;
using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.Arch {
	/// <summary>
	/// 记录包含了哪些村民的组件
	/// </summary>
	public class VillContainerComponent : IComponent {
		public List<ulong> VillGids = new();
		public bool IsDirty = true;
	}
}