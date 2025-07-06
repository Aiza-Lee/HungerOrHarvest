using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Common.Logic {
	public class RepoBuffComponent : IComponent {
		public EtList<RepoType, float> ProdBuff_F;
		public EtList<RepoType, float> ConsBuff_F;
	}	
}