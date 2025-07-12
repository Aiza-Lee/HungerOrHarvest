using System.Collections.Generic;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// 代表建筑销毁的资源
	/// </summary>
	public class ArchDestroyResource : IResource, ISaveableResource, IWorldClearRespondable {
		public List<ulong> ArchToDestroyGid = new();

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is ArchDestroyResource res) {
					ArchToDestroyGid.Clear();
					ArchToDestroyGid.AddRange(res.ArchToDestroyGid);
					break;
				}
			}
		}

		public void RespondWorldClear() {
			ArchToDestroyGid.Clear();
		}
	}
}