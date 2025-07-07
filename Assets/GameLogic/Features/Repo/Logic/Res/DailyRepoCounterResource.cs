using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	public class DailyRepoCounterResource : IResource {
		public EtList<RepoType, float> DailyProdSum_F = new(fillAll: true);
		public EtList<RepoType, float> DailyConsSum_F = new(fillAll: true);
		
		public void Reset() {
			DailyProdSum_F.Fill(0f);
			DailyConsSum_F.Fill(0f);
		}
	}
}