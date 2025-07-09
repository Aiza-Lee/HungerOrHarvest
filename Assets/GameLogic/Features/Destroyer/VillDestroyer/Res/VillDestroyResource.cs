using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// 代表建筑销毁的资源
	/// </summary>
	public class VillDestroyResource : IResource {
		public List<ulong> VillToDestroy = new();
	}
}