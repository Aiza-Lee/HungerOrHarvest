using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// VillPositionComponent 用于存储村民的上次移动时间。
	/// </summary>
	public class VillMoveComponent : IComponent {
		public ulong LastMoveTick;
	}
}