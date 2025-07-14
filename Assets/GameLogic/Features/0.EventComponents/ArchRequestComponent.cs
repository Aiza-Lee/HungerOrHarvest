using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Events {
	public class ArchTryProdRequestComponent : IComponent {
		public EtList<RepoType, float> Cons, Prod;
	}
	
}