using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	public class DailyRepoCounterResource : IResource, ISaveableResource {
		public EtList<RepoType, float> DailyProdSum_F = new(fillAll: true);
		public EtList<RepoType, float> DailyConsSum_F = new(fillAll: true);

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is DailyRepoCounterResource dailyRepoCounter) {
					DailyProdSum_F = dailyRepoCounter.DailyProdSum_F;
					DailyConsSum_F = dailyRepoCounter.DailyConsSum_F;
					break;
				}
			}
		}
	}
}