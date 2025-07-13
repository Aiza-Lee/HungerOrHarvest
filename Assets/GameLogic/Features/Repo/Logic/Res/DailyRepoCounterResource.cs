using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	public class DailyRepoCounterResource : IResource, ISaveableResource, IWorldClearRespondable {
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

		public void RespondWorldClear() {
			DailyProdSum_F.Fill(0f);
			DailyConsSum_F.Fill(0f);
		}
	}
}