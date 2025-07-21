using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// BondToArchComponent 用于存储村民与建筑之间的绑定关系。
	/// </summary>
	[System.Serializable]
	public class BondToArchComponent : IComponent {
		public ulong WorkArchGid = 0;
		public ulong HomeArchGid = 0;
	}

}