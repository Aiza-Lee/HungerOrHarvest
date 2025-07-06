using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// BondToArchComponent 用于存储村民与建筑之间的绑定关系。
	/// </summary>
	[System.Serializable]
	public class BondToArchComponent : IComponent {
		public EntityId WorkArchEntityId;
		public EntityId HomeArchEntityId;
	}
}