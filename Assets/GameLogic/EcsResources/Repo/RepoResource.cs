using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Resources.Repo {
	public class RepoResource : IResource {
		public EtList<RepoType, float> Repos { get; private set; }
		public void CopyFrom(IResource other) {
			throw new System.NotImplementedException();
		}
	}
}