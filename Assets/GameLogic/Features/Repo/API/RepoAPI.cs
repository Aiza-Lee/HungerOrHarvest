using GameLogic.Common.DataTypes;
using GameLogic.World;

namespace GameLogic.Features.Repo {
	public static class RepoQueryAPI {
		public static float GetRepoAmount(RepoType repoType) {
			return GameWorldMono.MainWorld.GetResource<RepoStatResource>().Repos_F[repoType];
		}
	}
}