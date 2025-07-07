using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	[System.Serializable]
	public class RepoStatResource : IResource {
		public EtList<RepoType, bool> Unlocked = new(fillAll: true);
		public EtList<RepoType, float> Repos_F = new(fillAll: true);
		public EtList<RepoType, float> RepoMax_F = new(fillAll: true);
	}
}