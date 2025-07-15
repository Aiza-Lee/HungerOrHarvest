using GameLogic.Common.DataTypes;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	public static class RepoQueryAPI {
		private static IWorld World => GameWorldMono.MainWorld;

		public static float GetRepoAmount(RepoType repoType) {
			return World.GetResource<RepoStatResource>().Repos_F[repoType];
		}
		public static ReadOnlyEtList<RepoType, float> GetAllRepoAmounts() {
			return World.GetResource<RepoStatResource>().Repos_F.AsReadOnly();
		}
		public static float GetRepoMaxAmount(RepoType repoType) {
			return World.GetResource<RepoStatResource>().RepoMax_F[repoType];
		}
		public static ReadOnlyEtList<RepoType, float> GetAllRepoMaxAmounts() {
			return World.GetResource<RepoStatResource>().RepoMax_F.AsReadOnly();
		}
	}
}