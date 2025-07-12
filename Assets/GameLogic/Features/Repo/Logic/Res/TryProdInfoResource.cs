using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	public class TryProdInfoResource : IResource, ISaveableResource, IWorldClearRespondable {
		public List<TryProdInfo> TryProdInfos = new();

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is TryProdInfoResource tryProdInfoResource) {
					TryProdInfos = tryProdInfoResource.TryProdInfos;
					break;
				}
			}
		}

		public void RespondWorldClear() {
			TryProdInfos.Clear();
		}
	}
	public class TryProdInfo {
		public ulong VillGid;
		public EtList<RepoType, float> Cons;
		public EtList<RepoType, float> Prod;
		public EtList<JobType, float> ExpAdd;
		public float VitCost;
	}
}