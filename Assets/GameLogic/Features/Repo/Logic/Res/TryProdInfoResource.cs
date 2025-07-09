using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	public class TryProdInfoResource : IResource {
		public List<TryProdInfo> TryProdInfos = new();
	}
	public class TryProdInfo {
		public bool Succeed;
		public EtList<RepoType, float> Cons;
		public EtList<RepoType, float> Prod;
	}
}