using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Common.Logic {
	/// <summary>
	/// RepoBuffComponent 资源生产和消耗的增益效果。
	/// </summary>
	[System.Serializable]
	public class RepoBuffComponent : IComponent {
		public EtList<RepoType, float> ProdBuff_F = new(fillAll: true);
		public EtList<RepoType, float> ConsBuff_F = new(fillAll: true);
	}	
}