using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	/// <summary>
	/// RepoStatResource 用于存储和管理资源库的状态信息。
	/// 包括资源库的解锁状态、当前资源量和最大资源量等
	/// </summary>
	[System.Serializable]
	public class RepoStatResource : IResource, ISaveableResource, IWorldClearRespondable {
		public EtList<RepoType, bool> Unlocked_F = new(fillAll: true);
		public EtList<RepoType, float> Repos_F = new(fillAll: true);
		public EtList<RepoType, float> RepoMax_F = new(fillAll: true);

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is RepoStatResource repoStat) {
					Unlocked_F = repoStat.Unlocked_F;
					Repos_F = repoStat.Repos_F;
					RepoMax_F = repoStat.RepoMax_F;
					break;
				}
			}
		}

		public void RespondWorldClear() {
			Unlocked_F.Fill(false);
			Repos_F.Fill(0f);
			RepoMax_F.Fill(0f);
		}
	}
}