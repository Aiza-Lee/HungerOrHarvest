using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	/// <summary>
	/// RepoStatResource 用于存储和管理资源库的状态信息。
	/// 包括资源库的解锁状态、当前资源量和最大资源量等
	/// </summary>
	[System.Serializable]
	public class RepoStatResource : IResource {
		public EtList<RepoType, bool> Unlocked = new(fillAll: true);
		public EtList<RepoType, float> Repos_F = new(fillAll: true);
		public EtList<RepoType, float> RepoMax_F = new(fillAll: true);
	}
}