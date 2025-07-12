using System.Collections.Generic;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// 代表建筑销毁的资源
	/// </summary>
	public class VillDestroyResource : IResource, IWorldClearRespondable {
		public List<ulong> VillToDestroy = new();

		public void RespondWorldClear() {
			VillToDestroy.Clear();
		}
	}
}