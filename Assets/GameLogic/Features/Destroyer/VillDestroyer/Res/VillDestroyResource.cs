using System.Collections.Generic;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// 代表建筑销毁的资源
	/// </summary>
	public class VillDestroyResource : IResource, IWorldClearRespondable, ISaveableResource {
		public List<ulong> VillToDestroy = new();

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is VillDestroyResource res) {
					VillToDestroy.Clear();
					VillToDestroy.AddRange(res.VillToDestroy);
					break;
				}
			}
		}

		public void RespondWorldClear() {
			VillToDestroy.Clear();
		}
	}
}